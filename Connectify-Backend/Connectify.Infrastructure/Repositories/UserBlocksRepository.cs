using Connectify.Application.Interfaces.RepositoriesInterfaces;
using Connectify.Domain.Entities;
using Connectify.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Infrastructure.Repositories
{
    public class UserBlocksRepository : IUserBlocksRepository
    {
        private readonly AppDbContext _context;
        public UserBlocksRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(UserBlocks item)
        {
            await _context.UserBlocks.AddAsync(item);
        }

        public IEnumerable<UserBlocks> Filter(Func<UserBlocks, bool> predicate)
        {
            return _context.UserBlocks.Where(predicate);
        }

        public async Task<IEnumerable<UserBlocks>> GetAllAsync()
        {
            return await _context.UserBlocks.ToListAsync();
        }

        public async Task RemoveBlockFromSpecificUserAsync(Guid blockerId, Guid blockedId)
        {
            await _context.UserBlocks.Where(x => x.BlockerId == blockerId && x.BlockedId == blockedId)
                                     .ExecuteDeleteAsync();
        }
    }
}
