using Connectify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.Interfaces.RepositoriesInterfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<Message?> GetMessageByIdAsync(Guid messageId);
        IEnumerable<Message> GetAllUserMessagesInChat(Guid chatId, Guid userId);
        Task RemoveMessageAsync(Guid messageId);
        void UpdateMessageAsync(Message message);
        Task<List<Message>?> UpdateMessagesSeenAsync(string[] messagesGuid);
    }
}
