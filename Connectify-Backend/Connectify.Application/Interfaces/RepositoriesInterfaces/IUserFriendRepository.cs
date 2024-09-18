using Connectify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.Interfaces.RepositoriesInterfaces
{
    public interface IUserFriendRepository : IRepository<UserFriend>
    {
        Task RemoveFriendAsync(Guid userId1, Guid userId2);
        Task<UserFriend?> GetFriend(Guid userId1, Guid userId2);
    }
}
