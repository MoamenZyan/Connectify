using Connectify.Application.DTOs;
using Connectify.Application.Interfaces.ApplicationServicesInterfaces;
using Connectify.Application.Interfaces.RepositoriesInterfaces;
using Connectify.Domain.Entities;
using Connectify.Domain.Enums;
using Connectify.Domain.Factories;
using Connectify.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Connectify.Application.Services.EntitiesApplicationServices
{
    public class ChatApplicationService : IChatApplicationService
    {
        private readonly IChatService _chatService;
        private readonly IChatRepository _chatRepository;
        private readonly IUserChatRepository _userChatRepository;
        private readonly IUserFriendRepository _userFriendRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserPrivateChatRepository _userPrivateChatRepository;
        public ChatApplicationService(IChatService chatService,
                                        IUserChatRepository userChatRepository,
                                        IChatRepository chatRepository,
                                        IUnitOfWork untiOfWork,
                                        IUserPrivateChatRepository userPrivateChatRepository,
                                        IUserFriendRepository userFriendRepository)
        {
            _chatService = chatService;
            _userChatRepository = userChatRepository;
            _chatRepository = chatRepository;
            _unitOfWork = untiOfWork;
            _userPrivateChatRepository = userPrivateChatRepository;
            _userFriendRepository = userFriendRepository;
        }
        public async Task<(bool, Guid)> CreateGroupChat(IFormCollection chatData, Guid currentUserId)
        {
            try
            {
                Chat chat = _chatService.CreateChat(ChatType.Group, Convert.ToString(chatData["Photo"]!).Substring(14), chatData);
                await _chatRepository.AddAsync(chat);

                UserChat userChat = UserChatsFactory.CreateUserChat(currentUserId, chat.Id, UserRole.Owner);
                await _userChatRepository.AddAsync(userChat);

                foreach (var member in Convert.ToString(chatData["MembersIds"]).Split(","))
                {
                    Console.WriteLine("---------------------> " + member);
                    UserChat memberChat = UserChatsFactory.CreateUserChat(new Guid(member!), chat.Id, UserRole.Member);
                    await _userChatRepository.AddAsync(memberChat);
                }

                await _unitOfWork.SaveChangesAsync();

                return (true, chat.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return (false, default);
            }
        }

        public async Task<(bool, Guid)> CreateNormalChat(Guid currentUserId, Guid chatUserId)
        {
           try
           {
                var IsFriend = _userFriendRepository.GetFriend(currentUserId, chatUserId);
                if (IsFriend == null)
                    return (false, default);

                Chat chat = _chatService.CreateChat(ChatType.Normal, "", null!);

                UserChat userChat1 = UserChatsFactory.CreateUserChat(currentUserId, chat.Id, UserRole.Member);
                UserChat userChat2 = UserChatsFactory.CreateUserChat(chatUserId, chat.Id, UserRole.Member);
                UserPrivateChat userPrivateChat = UserPrivateChatFactory.CreateUserPrivateChat(currentUserId, chatUserId, chat.Id);
                await _chatRepository.AddAsync(chat);
                await _userChatRepository.AddAsync(userChat1);
                await _userChatRepository.AddAsync(userChat2);
                await _userPrivateChatRepository.AddAsync(userPrivateChat);

                await _unitOfWork.SaveChangesAsync();
                
                return (true, chat.Id);
           }
           catch (Exception ex)
           {
               Console.WriteLine(ex);
               return (false, default);
           }
        }


        public Task<bool> DeleteChat(int chatId)
        {
            throw new NotImplementedException();
        }

        public async Task<ChatDto?> GetPrivateChat(Guid currentUserId, Guid receiverId)
        {
            var chat = await _userPrivateChatRepository.GetPrivateChat(currentUserId, receiverId);
            if (chat == null)
                return null;

            return new ChatDto(chat, currentUserId, Guid.Empty);
        }

        public Task<bool> UserJoinGroupChat(int chatId, int userId, UserRole role)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserLeaveGroupChat(int chatId, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
