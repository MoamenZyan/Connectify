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
    public class InfoNotificationRepository : INotificationRepository<InfoNotification>
    {
        private readonly AppDbContext _context;
        public InfoNotificationRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(InfoNotification item)
        {
            await _context.InfoNotifications.AddAsync(item);
        }

        public async Task DeleteNotification(Guid id)
        {
            await _context.InfoNotifications.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public IEnumerable<InfoNotification> Filter(Func<InfoNotification, bool> predicate)
        {
            return _context.InfoNotifications.Where(predicate);
        }

        public async Task<IEnumerable<InfoNotification>> GetAllAsync()
        {
            return await _context.InfoNotifications.ToListAsync();
        }

        public async Task<InfoNotification?> GetNotificationById(Guid id)
        {
            return await _context.InfoNotifications.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void UpdateNotification(InfoNotification notification)
        {
            _context.InfoNotifications.Update(notification);
        }
    }
}
