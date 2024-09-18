using Connectify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Domain.Factories
{
    public class UserFriendFactory
    {
        public static UserFriend CreateFriend(Guid senderId, Guid receiverId)
        {
            if (senderId == default || receiverId == default)
                throw new ArgumentNullException("parameter is null");

            if (senderId == receiverId)
                throw new ArgumentException("sender Id is equal to receiver Id");

            UserFriend userFriend = new UserFriend()
            {
                UserId1 = senderId,
                UserId2 = receiverId,
            };
            return userFriend;
        }
    }
}
