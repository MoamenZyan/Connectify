using Connectify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Domain.Factories
{
    public class UserPrivateChatFactory
    {
        public static UserPrivateChat CreateUserPrivateChat(Guid senderId, Guid receiverId, Guid chatId)
        {
            UserPrivateChat chat = new UserPrivateChat()
            {
                User1Id = senderId,
                User2Id = receiverId,
                ChatId = chatId
            };
            return chat;
        }
    }
}
