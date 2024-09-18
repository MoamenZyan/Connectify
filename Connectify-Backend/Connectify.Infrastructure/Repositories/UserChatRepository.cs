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
    public class UserChatRepository : IUserChatRepository
    {
        private readonly AppDbContext _context;
        public UserChatRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserChat item)
        {
            await _context.UserChats.AddAsync(item);
        }

        public IEnumerable<UserChat> Filter(Func<UserChat, bool> predicate)
        {
            return _context.UserChats.Where(predicate);
        }

        public async Task<IEnumerable<UserChat>> GetAllAsync()
        {
            return await _context.UserChats.ToListAsync();
        }

        public IEnumerable<Chat> GetAllUserJoinedChats(Guid userId)
        {
            return _context.UserChats
                           .Include(x => x.Chat)
                           .Where(x => x.UserId == userId)
                           .Select(x => x.Chat);
        }

        public IEnumerable<User>? GetAllUsersThatDoesntHaveChatWith(Guid userId)
        {
            return _context.UserChats.Include(x => x.User)
                                    .Where(x => x.UserId != userId)
                                    .Select(x => x.User);
        }

        public IEnumerable<UserChat?> GetUserChat(Guid userId, Guid chatId)
        {
            return _context.UserChats.Where(x => x.UserId == userId && x.ChatId == chatId);
        }

        public async Task RemoveUserFromChat(Guid userId, Guid chatId)
        {
            await _context.UserChats.Where(x => x.UserId == userId && x.ChatId == chatId)
                                        .ExecuteDeleteAsync();
        }
    }
}
