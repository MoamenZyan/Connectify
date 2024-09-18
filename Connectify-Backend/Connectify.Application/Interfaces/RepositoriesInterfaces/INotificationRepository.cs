using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.Interfaces.RepositoriesInterfaces
{
    public interface INotificationRepository<T> : IRepository<T> where T : class
    {
        Task DeleteNotification(Guid id);
        void UpdateNotification(T notification);
        Task<T?> GetNotificationById(Guid id);
    }
}
