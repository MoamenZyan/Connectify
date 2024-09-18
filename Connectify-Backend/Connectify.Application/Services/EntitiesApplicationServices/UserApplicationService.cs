using Connectify.Application.Interfaces.ApplicationServicesInterfaces;
using Connectify.Application.Interfaces.RepositoriesInterfaces;
using Connectify.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Connectify.Domain.Enums;
using Connectify.Domain.Factories;
using Connectify.Application.DTOs;
using Connectify.Application.Interfaces.UtilitesInterfaces;
using Connectify.Domain.Entities;
using Connectify.Application.Interfaces.AWSServicesInterfaces;
using Connectify.Application.Interfaces.HubInterfaces;
using Hangfire;
using Connectify.Application.Interfaces.RedisInterfaces;
using Newtonsoft.Json;

namespace Connectify.Application.Services.EntitiesApplicationServices
{
    public class UserApplicationService : IUserApplicationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFriendRequestRepository _friendRequestRepository;
        private readonly IUserFriendRepository _userFriendRepository;
        private readonly IUserBlocksRepository _userBlocksRepository;
        private readonly IChatApplicationService _chatApplicationService;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJWTService _jwtService;
        private readonly INotificationApplicationService _notificationApplicationService;
        private readonly IPhotoService _photoService;
        private readonly IRedis _redis;
        

        public UserApplicationService(IUserRepository userRepository,
                                    IUserService userService,
                                    IFriendRequestRepository friendRequestRepository,
                                    IUserFriendRepository userFriendRepository,
                                    IUnitOfWork unitOfWork,
                                    IUserBlocksRepository userBlocksRepository,
                                    IJWTService jwtService,
                                    INotificationApplicationService notificationApplicationService,
                                    IPhotoService photoService,
                                    IChatApplicationService chatApplicationService,
                                    IRedis redis)
        {
            _userRepository = userRepository;
            _userService = userService;
            _friendRequestRepository = friendRequestRepository;
            _userFriendRepository = userFriendRepository;
            _unitOfWork = unitOfWork;
            _userBlocksRepository = userBlocksRepository;
            _jwtService = jwtService;
            _notificationApplicationService = notificationApplicationService;
            _photoService = photoService;
            _chatApplicationService = chatApplicationService;
            _redis = redis;
        }

        // Accept Friend Request
        public async Task<bool> AcceptFriendRequest(Guid currentUserId, Guid senderId)
        {
            var friendRequest = await _friendRequestRepository.GetFriendRequest(senderId, currentUserId);
            if (friendRequest == null)
                throw new Exception("friend request doesn't exists");

            friendRequest.Status = FriendRequestStatus.Accepted;
            var userFriend = UserFriendFactory.CreateFriend(senderId, currentUserId);
            try
            {
                _friendRequestRepository.UpdateFriendRequest(friendRequest);
                await _userFriendRepository.AddAsync(userFriend);
                await _chatApplicationService.CreateNormalChat(currentUserId, senderId);
                await _unitOfWork.SaveChangesAsync();
                var currentUser = await _userRepository.GetMinimalUserByIdAsync(currentUserId);
                var receiverUser = await _userRepository.GetMinimalUserByIdAsync(senderId);

                await _redis.FlushAll();
                BackgroundJob.Enqueue(() => _notificationApplicationService.FriendRequestAcceptedNotification(new UserDto(currentUser!), new UserDto(receiverUser!)));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        // Block user
        public async Task<bool> BlockUser(Guid currentUserId, Guid blockedId)
        {
            var userBlock = UserBlocksFactory.CreateUserBlock(currentUserId, blockedId);
            try
            {
                await _userBlocksRepository.AddAsync(userBlock);
                await _unitOfWork.SaveChangesAsync();
                await Task.WhenAll(_redis.Delete($"user:{currentUserId}"), _redis.Delete($"user:{blockedId}"));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

        }

        // Decline friend request
        public async Task<bool> DeclineFriendRequest(Guid currentUserId, Guid senderId)
        {
            try
            {
                await _friendRequestRepository.DeleteFriendRequest(senderId, currentUserId);
                await Task.WhenAll(_redis.Delete($"user:{currentUserId}"), _redis.Delete($"user:{senderId}"));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        // Delete user
        public async Task<bool> DeleteUser(Guid currentUserId)
        {
            try
            {
                await _userRepository.DeleteUserAsync(currentUserId);
                await _redis.Delete($"user:{currentUserId}");
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        // Get all users dtos
        public async Task<List<UserDto>?> GetAllUsers(Guid currentUserId)
        {
            try
            {
                var usersFromRedis = await _redis.Get<List<User>?>("users");
                if (usersFromRedis != null)
                    return usersFromRedis.Where(x => x.Id != currentUserId).Select(x => new UserDto(x)).ToList();


                var users = await _userRepository.GetAllAsync();
                await _redis.Set("users", users, TimeSpan.FromMinutes(30));

                return users.Where(x => x.Id != currentUserId).Select(x => new UserDto(x)).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        // Get current user (Logged in user)
        public async Task<UserDto?> GetCurrentUser(Guid userId)
        {
            var userFromRedis = await _redis.Get<User>($"user:{userId}");
            if (userFromRedis != null)
                return new UserDto(userFromRedis);

            var result = await _userRepository.GetFullUserByIdAsync(userId);
            if (result == null)
                return null;

            await _redis.Set($"user:{userId}", result, TimeSpan.FromMinutes(30));
            return new UserDto(result);
        }

        // Login user => generate token
        public async Task<string?> LoginUser(Guid userId)
        {
            var user = await _userRepository.GetMinimalUserByIdAsync(userId);
            if (user == null)
                return null;

            var token = _jwtService.GenerateToken(userId, user.Email, user.Phone);
            return token;
        }

        // Create new user
        public async Task<(bool, string)> RegisterUser(IFormCollection form)
        {
            try
            {
                var user = await _userService.CreateUser(form, _userRepository.GetUserByEmailAsync, _userRepository.GetUserByPhoneAsync);
                await _userRepository.AddAsync(user);


                await _unitOfWork.SaveChangesAsync();

                BackgroundJob.Schedule(() =>
                _notificationApplicationService.SendWelcomeNotification(user),
                TimeSpan.FromMinutes(1));

                return (true, "user created");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return (false, ex.Message);
            }
        }

        // Remove friend request
        public async Task<bool> RemoveFriendRequest(Guid currentUserId, Guid senderId)
        {
            try
            {
                await _friendRequestRepository.DeleteFriendRequest(senderId, currentUserId);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        // Search users by name
        public List<UserDto>? SearchByUserName(string userName, Guid currentUserId)
        {
            var result = _userRepository.SearchByName(userName);
            if (result == null || result?.Count() == 0)
                return null;

            return result?.Where(x => x.Id != currentUserId).Select(x => new UserDto(x)).ToList();
        }

        // send friend request
        public async Task<bool> SendFriendRequest(Guid currentUserId, Guid receiverId)
        {
            var receiver = await _userRepository.GetMinimalUserByIdAsync(receiverId);
            var sender = await _userRepository.GetMinimalUserByIdAsync(currentUserId);

            if (receiver == null || sender == null)
                return false;

            var friendRequest = FriendRequestFactory.CreateFriendRequest(currentUserId, receiverId);

            try
            {
                await _friendRequestRepository.AddAsync(friendRequest);
                await _unitOfWork.SaveChangesAsync();

                await Task.WhenAll(_redis.Delete($"user:{currentUserId}"), _redis.Delete($"user:{receiverId}"));
                BackgroundJob.Enqueue(() => _notificationApplicationService.ReceivedFriendRequestNotification(new UserDto(sender), new UserDto(receiver)));

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        // Unblock user
        public async Task<bool> UnblockUser(Guid currentUserId, Guid blockedId)
        {
            try
            {
                await _userBlocksRepository.RemoveBlockFromSpecificUserAsync(currentUserId, blockedId);
                await Task.WhenAll(_redis.Delete($"user:{currentUserId}"), _redis.Delete($"user:{blockedId}"));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        // Update profile photo
        public async Task<bool> UpdateProfilePhoto(IFormFile photo, Guid userId)
        {
            try
            {
                var result = await _photoService.UploadPhoto(photo, userId);
                var user = await _userRepository.GetMinimalUserByIdAsync(userId);
                if (user == null)
                    return false;

                user.Photo = result;
                await _unitOfWork.SaveChangesAsync();
                await _redis.FlushAll();
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        // Update user info
        public async Task<(bool, string)> UpdateUser(Guid currentUserId, IFormCollection form)
        {
            var originalUser = await _userRepository.GetMinimalUserByIdAsync(currentUserId);
            if (originalUser == null)
                return (false, "user isn't exists to update");
            
            try
            {
                var updatedUser = await _userService.CreateUser(form, _userRepository.GetUserByEmailAsync, _userRepository.GetUserByPhoneAsync);
                _userService.UpdateUser(originalUser, updatedUser);

                _userRepository.UpdateUser(originalUser);
                await _unitOfWork.SaveChangesAsync();
                await _redis.Delete($"user:{currentUserId}");

                return (true, "user updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return (false, ex.Message);
            }
        }

        // User became offline
        public async Task<bool> UserIsOffline(Guid userId)
        {
            try
            {
                var userFromRedis = await _redis.Get<User>($"user:{userId}");
                if (userFromRedis != null)
                {
                    userFromRedis.IsOnline = false;
                    await _redis.Set($"user:{userId}", userFromRedis, TimeSpan.FromMinutes(30));
                    _userRepository.UpdateUser(userFromRedis);
                    await _unitOfWork.SaveChangesAsync();
                    return true;
                }
                else
                {
                    var user = await _userRepository.GetMinimalUserByIdAsync(userId);
                    if (user == null)
                        return false;

                    user.IsOnline = false;
                    return true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        // User became online
        public async Task<List<MessageDto>?> UserIsOnline(Guid userId)
        {
            try
            {
                var userFromRedis = await _redis.Get<User>($"user:{userId}");
                if (userFromRedis != null)
                {
                    userFromRedis.IsOnline = true;
                    await _redis.Set($"user:{userId}", userFromRedis, TimeSpan.FromMinutes(30));
                    _userRepository.UpdateUser(userFromRedis);
                }
                else
                {
                    var user = await _userRepository.GetFullUserByIdAsync(userId);

                    if (user == null)
                        return null;

                    user.IsOnline = true;
                }
                var messages = await _userRepository.GetAllReceivedMessages(userId);
                if (messages == null)
                    return null;

                foreach (var message in messages)
                    message.Status = MessageStatus.Sent;

                return messages.Select(x => new MessageDto(x)).ToList();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
