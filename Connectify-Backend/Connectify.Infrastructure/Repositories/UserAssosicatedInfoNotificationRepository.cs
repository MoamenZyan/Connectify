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
    public class UserAssosicatedInfoNotificationRepository : IUserNotificationRepository<UserAssociatedInfoNotification>
    {
        private readonly AppDbContext _context;
        public UserAssosicatedInfoNotificationRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(UserAssociatedInfoNotification item)
        {
            await _context.UserAssociatedInfoNotifications.AddAsync(item);
        }

        public async Task DeleteNotificationFromUser(Guid userId, Guid notificationId)
        {
            await _context.UserAssociatedInfoNotifications.Where(x => x.UserId == userId && x.NotificationId == notificationId)
                        .ExecuteDeleteAsync();
        }

        public IEnumerable<UserAssociatedInfoNotification> Filter(Func<UserAssociatedInfoNotification, bool> predicate)
        {
            return _context.UserAssociatedInfoNotifications.Where(predicate);
        }

        public async Task<IEnumerable<UserAssociatedInfoNotification>> GetAllAsync()
        {
            return await _context.UserAssociatedInfoNotifications.ToListAsync();
        }

        public async Task<UserAssociatedInfoNotification?> GetUserNotification(Guid userId, Guid notificationId)
        {
            return await _context.UserAssociatedInfoNotifications.FirstOrDefaultAsync(x => x.UserId == userId && x.NotificationId == notificationId);
        }
    }
}
