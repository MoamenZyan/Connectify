using Connectify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connectify.Domain.Enums;

namespace Connectify.Application.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Photo { get; set; } = null!;
        public bool IsVerified { get; set; } = false;
        public bool IsOnline { get; set; } = false;


        public List<ChatDto> PrivateChats { get; set; } = new List<ChatDto>();
        public List<ChatDto> GroupChats { get; set; } = new List<ChatDto>();
        public List<UserMinimalDto> BlockedUsers { get; set; } = new List<UserMinimalDto>();
        public List<UserMinimalDto> Friends { get; set; } = new List<UserMinimalDto>();
        public Dictionary<Guid, SentFriendRequestDto> SentFriendRequests { get; set; } = new Dictionary<Guid, SentFriendRequestDto>();
        public Dictionary<Guid, ReceivedFriendRequestDto> ReceivedFriendRequests { get; set; } = new Dictionary<Guid, ReceivedFriendRequestDto>();
        public List<AssociatedInfoNotificationDto> AssociatedInfoNotifications { get; set; } = new List<AssociatedInfoNotificationDto>();
        public List<InfoNotificationDto> InfoNotifications { get; set; } = new List<InfoNotificationDto>();
        public UserDto(User user)
        {
            if (user == null)
                return;
            Id = user.Id;
            FullName = $"{user.Fname} {user.Lname}";
            Email = user.Email;
            Phone = user.Phone;
            Photo = user.Photo;
            IsVerified = user.IsVerified;
            IsOnline = user.IsOnline;

            PrivateChats = user.UserJoinedChats.Where(x => x.Chat.Type == ChatType.Normal)
                                                .Select(x => new ChatDto(x.Chat, user.Id, Guid.Empty)).ToList();

            GroupChats = user.UserJoinedChats.Where(x => x.Chat.Type == ChatType.Group)
                                                .Select(x => new ChatDto(x.Chat, user.Id, x.Chat.Users.FirstOrDefault(o => o.Role == UserRole.Owner)!.UserId)).ToList();

            BlockedUsers = user.BlockedUsers.Select(x => new UserMinimalDto(x.BlockerUser)).ToList();

            Friends = user.Friends.Select(x => new UserMinimalDto(x.User2))
                    .Concat(user.FriendOf.Select(x => new UserMinimalDto(x.User1))).ToList();

            SentFriendRequests = user.SentFriendRequests.Select(x => new SentFriendRequestDto(x)).ToDictionary(x => x.ReceiverId, x => x);
            ReceivedFriendRequests = user.ReceivedFriendRequests.Select(x => new ReceivedFriendRequestDto(x)).ToDictionary(x => x.SenderId, x => x);
            AssociatedInfoNotifications = user.UserAssociatedInfoNotifications.Select(x => new AssociatedInfoNotificationDto(x.Notification.AssoicatedUser.Id, x.Notification.AssoicatedUser.Fname + " " + x.Notification.AssoicatedUser.Lname, x.Notification.AssoicatedUser.Photo, x.Notification)).ToList();
            InfoNotifications = user.UserInfoNotifications.Select(x => new InfoNotificationDto(x.Notification)).ToList();
        }
    }
    public class UserMinimalDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Photo { get; set; } = null!;
        public bool IsOnline { get; set; }
        public UserMinimalDto(User user)
        {
            if (user == null)
                return;
            Id = user.Id;
            FullName = $"{user.Fname} {user.Lname}";
            Email = user.Email;
            Phone = user.Phone;
            Photo = user.Photo;
            IsOnline = user.IsOnline;
        }
    }
}
