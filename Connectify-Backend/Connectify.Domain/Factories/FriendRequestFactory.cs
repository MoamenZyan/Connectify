using Connectify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Domain.Factories
{
    public class FriendRequestFactory
    {
        public static FriendRequest CreateFriendRequest(Guid senderId, Guid receiverId)
        {
            FriendRequest friendRequest = new FriendRequest()
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                CreatedAt = DateTime.Now,
                Status = Enums.FriendRequestStatus.Pending,
            };

            return friendRequest;
        }
    }
}
