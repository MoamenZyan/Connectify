using Connectify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.Interfaces.RepositoriesInterfaces
{
    public interface IUserPrivateChatRepository : IRepository<UserPrivateChat>
    {
        Task<Chat?> GetPrivateChat(Guid senderId, Guid receiverId);
        Task DeleteTwoUsersChat(Guid senderId, Guid receiverId);
    }
}
