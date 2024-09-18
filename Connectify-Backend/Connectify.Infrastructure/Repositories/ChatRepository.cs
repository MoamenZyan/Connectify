using Connectify.Application.Interfaces.RepositoriesInterfaces;
using Connectify.Domain.Entities;
using Connectify.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Connectify.Infrastructure.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly AppDbContext _context;
        public ChatRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Chat item)
        {
            await _context.Chats.AddAsync(item);
        }

        public async Task DeleteChatAsync(Guid chatId)
        {
            await _context.Chats.Where(x => x.Id == chatId).ExecuteDeleteAsync();
        }

        public IEnumerable<Chat> Filter(Func<Chat, bool> predicate)
        {
            return _context.Chats.Where(predicate);
        }

        public async Task<IEnumerable<Chat>> GetAllAsync()
        {
            return await _context.Chats.ToListAsync();
        }

        public async Task<Chat?> GetChatByIdAsync(Guid chatId)
        {
            return await _context.Chats.FirstOrDefaultAsync(x => x.Id == chatId);
        }

        public void UpdateChatAsync(Chat chat)
        {
            _context.Chats.Update(chat);
        }
    }
}
