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
    public class UserFriendRepository : IUserFriendRepository
    {
        private readonly AppDbContext _context;
        public UserFriendRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(UserFriend item)
        {
            await _context.UserFriends.AddAsync(item);
        }

        public IEnumerable<UserFriend> Filter(Func<UserFriend, bool> predicate)
        {
            return _context.UserFriends.Where(predicate);
        }

        public async Task<IEnumerable<UserFriend>> GetAllAsync()
        {
            return await _context.UserFriends.ToListAsync();
        }

        public async Task<UserFriend?> GetFriend(Guid userId1, Guid userId2)
        {
            return await _context.UserFriends.FirstOrDefaultAsync(x => (x.UserId1 == userId1 && x.UserId2 == userId2) ||
                                                            (x.UserId1 == userId2 && x.UserId2 == userId1));
        }

        public async Task RemoveFriendAsync(Guid userId1, Guid userId2)
        {
            await _context.UserFriends.Where(x => x.UserId1 == userId1 && x.UserId2 == userId2).ExecuteDeleteAsync();
        }
    }
}
