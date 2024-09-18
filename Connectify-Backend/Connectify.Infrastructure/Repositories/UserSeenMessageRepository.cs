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
    public class UserSeenMessageRepository : IUserSeenMessageRepository
    {
        private readonly AppDbContext _context;
        public UserSeenMessageRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(UserSeenMessage item)
        {
            await _context.UserSeenMessages.AddAsync(item);
        }

        public IEnumerable<UserSeenMessage> Filter(Func<UserSeenMessage, bool> predicate)
        {
            return _context.UserSeenMessages.Where(predicate);
        }

        public async Task<IEnumerable<UserSeenMessage>> GetAllAsync()
        {
            return await _context.UserSeenMessages.ToListAsync();
        }

        public IEnumerable<User> GetAllUsersThatSeenMessage(Guid messageId)
        {
            return _context.UserSeenMessages.Include(x => x.User)
                                            .Where(x => x.MessageId == messageId)
                                            .Select(x => x.User);
        }
    }
}
