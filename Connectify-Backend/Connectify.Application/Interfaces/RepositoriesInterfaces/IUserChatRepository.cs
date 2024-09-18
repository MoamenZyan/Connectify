using Connectify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.Interfaces.RepositoriesInterfaces
{
    public interface IUserChatRepository : IRepository<UserChat>
    {
        Task RemoveUserFromChat(Guid userId, Guid chatId);
        IEnumerable<Chat>? GetAllUserJoinedChats(Guid userId);
        IEnumerable<User>? GetAllUsersThatDoesntHaveChatWith(Guid userId);
        IEnumerable<UserChat?> GetUserChat(Guid userId, Guid chatId);
    }
}
