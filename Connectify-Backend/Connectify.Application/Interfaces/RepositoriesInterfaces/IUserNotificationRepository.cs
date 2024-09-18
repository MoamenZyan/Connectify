using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.Interfaces.RepositoriesInterfaces
{
    public interface IUserNotificationRepository<T> : IRepository<T> where T : class
    {
        Task<T?> GetUserNotification(Guid userId, Guid notificationId);
        Task DeleteNotificationFromUser(Guid userId, Guid notificationId);
    }
}
