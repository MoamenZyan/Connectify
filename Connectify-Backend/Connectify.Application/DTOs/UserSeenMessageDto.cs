using Connectify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.DTOs
{
    public class UserSeenMessageDto
    {
        public Guid UserId { get; set; }
        public Guid MessageId { get; set; }
        public DateTime SeenAt { get; set; }
        public string FullName { get; set; } = null!;

        public UserSeenMessageDto(UserSeenMessage userSeenMessage)
        {
            UserId = userSeenMessage.UserId;
            MessageId = userSeenMessage.MessageId;
            SeenAt = userSeenMessage.SeenAt;
            FullName = $"{userSeenMessage.User.Fname} {userSeenMessage.User.Lname}";
        }
    }
}
