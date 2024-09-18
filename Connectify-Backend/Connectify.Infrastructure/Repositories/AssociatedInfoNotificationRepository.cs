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
    public class AssociatedInfoNotificationRepository : INotificationRepository<AssociatedInfoNotification>
    {
        private readonly AppDbContext _context;
        public AssociatedInfoNotificationRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(AssociatedInfoNotification item)
        {
            await _context.AssociatedInfoNotifications.AddAsync(item);
        }

        public async Task DeleteNotification(Guid id)
        {
             await _context.AssociatedInfoNotifications.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public IEnumerable<AssociatedInfoNotification> Filter(Func<AssociatedInfoNotification, bool> predicate)
        {
            return _context.AssociatedInfoNotifications.Where(predicate);
        }

        public async Task<IEnumerable<AssociatedInfoNotification>> GetAllAsync()
        {
            return await _context.AssociatedInfoNotifications.ToListAsync();
        }

        public async Task<AssociatedInfoNotification?> GetNotificationById(Guid id)
        {
            return await _context.AssociatedInfoNotifications.Include(x => x.AssoicatedUser)
                                                                .ThenInclude(x => x.SentFriendRequests)
                                                             .FirstOrDefaultAsync(x => x.Id == id);
        }

        public void UpdateNotification(AssociatedInfoNotification notification)
        {
            _context.AssociatedInfoNotifications.Update(notification);
        }
    }
}
