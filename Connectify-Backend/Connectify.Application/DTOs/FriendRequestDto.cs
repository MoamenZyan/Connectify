using Connectify.Domain.Entities;
using Connectify.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.DTOs
{
    public class SentFriendRequestDto
    {
        public Guid ReceiverId { get; set; } = default;
        public DateTime CreatedAt { get; set; } = default;
        public FriendRequestStatus Status { get; set; } = default;

        public SentFriendRequestDto(FriendRequest friendRequest)
        {
            if (friendRequest == null)
                return;

            ReceiverId = friendRequest.ReceiverId;
            CreatedAt = friendRequest.CreatedAt;
            Status = friendRequest.Status;
        }
    }

    public class ReceivedFriendRequestDto
    {
        public Guid SenderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public FriendRequestStatus Status { get; set; }

        public ReceivedFriendRequestDto(FriendRequest friendRequest)
        {
            if (friendRequest == null) 
                return;
            SenderId = friendRequest.SenderId;
            CreatedAt = friendRequest.CreatedAt;
            Status = friendRequest.Status;
        }
    }
}
