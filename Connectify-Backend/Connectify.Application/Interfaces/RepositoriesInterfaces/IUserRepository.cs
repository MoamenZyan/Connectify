using Connectify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.Interfaces.RepositoriesInterfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetFullUserByIdAsync(Guid id);
        Task<User?> GetMinimalUserByIdAsync(Guid id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByPhoneAsync(string phone);
        Task UserIsOnline(Guid userId);
        Task<List<Message>?> GetAllReceivedMessages(Guid userId);
        IEnumerable<User>? SearchByName(string name);
        Task DeleteUserAsync(Guid id);
        void UpdateUser(User user);
    }
}
