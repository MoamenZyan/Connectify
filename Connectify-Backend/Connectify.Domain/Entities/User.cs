using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Connectify.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Fname { get; set; } = null!;
        public string Lname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Photo { get; set; } = "";
        public bool IsVerified { get; set; } = false;
        public bool IsOnline { get; set; } = false;

        [JsonIgnore]
        public virtual List<UserChat> UserJoinedChats { get; set; } = new List<UserChat>();
        [JsonIgnore]
        public virtual List<Message> Messages { get; set; } = new List<Message>();
        [JsonIgnore]
        public virtual List<UserBlocks> BlockedUsers { get; set; } = new List<UserBlocks>();
        [JsonIgnore]
        public virtual List<UserBlocks> BlockedFrom { get; set; } = new List<UserBlocks>();
        [JsonIgnore]
        public virtual List<UserSeenMessage> SeenMessages { get; set; } = new List<UserSeenMessage>();
        [JsonIgnore]
        public virtual List<UserFriend> Friends { get; set; } = new List<UserFriend>();
        [JsonIgnore]
        public virtual List<UserFriend> FriendOf { get; set; } = new List<UserFriend>();
        [JsonIgnore]
        public virtual List<UserPrivateChat> AsSender { get; set; } = new List<UserPrivateChat>();
        [JsonIgnore]
        public virtual List<UserPrivateChat> AsReceiver { get; set; } = new List<UserPrivateChat>();
        [JsonIgnore]
        public virtual List<FriendRequest> SentFriendRequests { get; set; } = new List<FriendRequest>();
        [JsonIgnore]
        public virtual List<FriendRequest> ReceivedFriendRequests { get; set; } = new List<FriendRequest>();
        [JsonIgnore]
        public virtual List<AssociatedInfoNotification> AssociatedInfoNotifications { get; set; } = new List<AssociatedInfoNotification>();
        [JsonIgnore]
        public virtual List<UserInfoNotification> UserInfoNotifications { get; set; } = new List<UserInfoNotification>();
        [JsonIgnore]
        public virtual List<UserAssociatedInfoNotification> UserAssociatedInfoNotifications { get; set; } = new List<UserAssociatedInfoNotification>();
    }
}
