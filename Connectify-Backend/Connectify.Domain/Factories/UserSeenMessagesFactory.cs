using Connectify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Domain.Factories
{
    public class UserSeenMessagesFactory
    {
        public static UserSeenMessage CreateUserSeenMessage(Guid userId, Guid messageId)
        {
            UserSeenMessage userSeenMessage = new UserSeenMessage()
            {
                UserId = userId,
                MessageId = messageId,
                SeenAt = DateTime.Now,
            };
            return userSeenMessage;
        }
    }
}
