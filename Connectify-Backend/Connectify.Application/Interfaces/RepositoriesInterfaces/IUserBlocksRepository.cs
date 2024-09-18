using Connectify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.Interfaces.RepositoriesInterfaces
{
    public interface IUserBlocksRepository : IRepository<UserBlocks>
    {
        Task RemoveBlockFromSpecificUserAsync(Guid blockerId, Guid blockedId);
    }
}
