using Connectify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.Interfaces.RepositoriesInterfaces
{
    public interface IFriendRequestRepository : IRepository<FriendRequest>
    {
        Task DeleteFriendRequest(Guid senderId, Guid receiverId);
        IEnumerable<FriendRequest> GetAllUserSentFriendRequest(Guid senderId);
        IEnumerable<FriendRequest> GetAllUserReceivedFriendRequest(Guid receiverId);
        Task<FriendRequest?> GetFriendRequest(Guid senderId, Guid receiverId);
        void UpdateFriendRequest(FriendRequest friendRequest);
    }
}
