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
    public class UserInfoNotificationRepository : IUserNotificationRepository<UserInfoNotification>
    {
        private readonly AppDbContext _context;
        public UserInfoNotificationRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(UserInfoNotification item)
        {
            await _context.UserInfoNotifications.AddAsync(item);
        }

        public async Task DeleteNotificationFromUser(Guid userId, Guid notificationId)
        {
            await _context.UserInfoNotifications.Where(x => x.UserId == userId && x.NotificationId == notificationId)
                        .ExecuteDeleteAsync();
        }

        public IEnumerable<UserInfoNotification> Filter(Func<UserInfoNotification, bool> predicate)
        {
            return _context.UserInfoNotifications.Where(predicate);
        }

        public async Task<IEnumerable<UserInfoNotification>> GetAllAsync()
        {
            return await _context.UserInfoNotifications.ToListAsync();
        }

        public async Task<UserInfoNotification?> GetUserNotification(Guid userId, Guid notificationId)
        {
            return await _context.UserInfoNotifications.FirstOrDefaultAsync(x => x.UserId == userId && x.NotificationId == notificationId);
        }
    }
}
