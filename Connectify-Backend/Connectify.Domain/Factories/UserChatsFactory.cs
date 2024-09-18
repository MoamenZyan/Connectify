using Connectify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Domain.Factories
{
    public class UserChatsFactory
    {
        public static UserChat CreateUserChat(Guid userId, Guid chatId, Enums.UserRole userRole)
        {
            UserChat chat = new UserChat()
            {
                UserId = userId,
                ChatId = chatId,
                Role = userRole,
            };
            return chat;
        }
    }
}
