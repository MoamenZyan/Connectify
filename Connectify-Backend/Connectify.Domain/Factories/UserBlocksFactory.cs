using Connectify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Domain.Factories
{
    public class UserBlocksFactory
    {
        public static UserBlocks CreateUserBlock(Guid blockerId, Guid blockedId)
        {
            UserBlocks userBlocks = new UserBlocks()
            {
                BlockerId = blockerId,
                BlockedId = blockedId
            };

            return userBlocks;
        }
    }
}
