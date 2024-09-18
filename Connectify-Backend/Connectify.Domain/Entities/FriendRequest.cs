using Connectify.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Connectify.Domain.Entities
{
    public class FriendRequest
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public DateTime CreatedAt { get; set; }
        public FriendRequestStatus Status { get; set; }

        [JsonIgnore]
        public User Sender { get; set; } = null!;
        [JsonIgnore]
        public User Receiver { get; set; } = null!;
    }
}
