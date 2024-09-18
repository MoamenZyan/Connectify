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
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(User item)
        {
            await _context.Users.AddAsync(item);
        }

        public async Task DeleteUserAsync(Guid id)
        {
                await _context.Users.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public IEnumerable<User> Filter(Func<User, bool> predicate)
        {
            return _context.Users.Where(predicate);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User?> GetMinimalUserByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User?> GetFullUserByIdAsync(Guid id)
        {
            return await _context.Users.Include(u => u.AssociatedInfoNotifications)
                                        .Include(u => u.UserJoinedChats)
                                            .ThenInclude(x => x.Chat)
                                                .ThenInclude(x => x.Users)
                                                    .ThenInclude(x => x.User)
                                        .Include(u => u.UserJoinedChats)
                                            .ThenInclude(x => x.Chat)
                                                .ThenInclude(x => x.Messages)
                                        .Include(u => u.BlockedUsers)
                                            .ThenInclude(x => x.BlockedUser)
                                        .Include(u => u.BlockedFrom)
                                            .ThenInclude(x => x.BlockerUser)
                                        .Include(u => u.SeenMessages)
                                            .ThenInclude(x => x.Message)
                                        .Include(u => u.UserInfoNotifications)
                                            .ThenInclude(x => x.Notification)
                                        .Include(u => u.UserAssociatedInfoNotifications)
                                            .ThenInclude(x => x.Notification)
                                                .ThenInclude(x => x.AssoicatedUser)
                                        .Include(x => x.SentFriendRequests)
                                        .Include(x => x.ReceivedFriendRequests)
                                        .AsSplitQuery()
                                        .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User?> GetUserByPhoneAsync(string phone)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Phone == phone);
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }

        public IEnumerable<User>? SearchByName(string name)
        {
            return _context.Users.Where(x => (x.Fname + " " + x.Lname).Contains(name));
        }

        public async Task<List<Message>?> GetAllReceivedMessages(Guid userId)
        {
            var user = await _context.Users.Include(x => x.UserJoinedChats)
                                            .ThenInclude(x => x.Chat)
                                            .ThenInclude(x => x.Messages)
                                            .FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
                return null;

            return user.UserJoinedChats.SelectMany(x => x.Chat.Messages).Where(x => x.SenderId != userId &&
                                                    x.Status == Domain.Enums.MessageStatus.Saved)
                                                    .ToList();

        }

        public async Task UserIsOnline(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) return;
            user.IsOnline = true;
        }
    }
}
