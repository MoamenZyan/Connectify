using Connectify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.Interfaces.RepositoriesInterfaces
{
    public interface IChatRepository : IRepository<Chat>
    {
        Task<Chat?> GetChatByIdAsync(Guid chatId);
        Task DeleteChatAsync(Guid chatId);
        void UpdateChatAsync(Chat chat);
    }
}
