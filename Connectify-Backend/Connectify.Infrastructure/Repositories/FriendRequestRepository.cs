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
    public class FriendRequestRepository : IFriendRequestRepository
    {
        private readonly AppDbContext _context;
        public FriendRequestRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(FriendRequest item)
        {
            await _context.FriendRequests.AddAsync(item);
        }

        public async Task DeleteFriendRequest(Guid senderId, Guid receiverId)
        {
            await _context.FriendRequests.Where(x => x.SenderId == senderId && x.ReceiverId == receiverId ||
                                                x.SenderId == receiverId && x.ReceiverId == senderId)
                                        .ExecuteDeleteAsync();
        }

        public IEnumerable<FriendRequest> Filter(Func<FriendRequest, bool> predicate)
        {
            return _context.FriendRequests.Where(predicate);
        }

        public async Task<IEnumerable<FriendRequest>> GetAllAsync()
        {
            return await _context.FriendRequests.ToListAsync();
        }

        public IEnumerable<FriendRequest> GetAllUserReceivedFriendRequest(Guid receiverId)
        {
            return _context.FriendRequests.Where(x => x.ReceiverId == receiverId);
        }

        public IEnumerable<FriendRequest> GetAllUserSentFriendRequest(Guid senderId)
        {
            return _context.FriendRequests.Where(x => x.SenderId == senderId);
        }

        public async Task<FriendRequest?> GetFriendRequest(Guid senderId, Guid receiverId)
        {
            return await _context.FriendRequests.FirstOrDefaultAsync(x => x.SenderId == senderId && x.ReceiverId == receiverId);
        }

        public void UpdateFriendRequest(FriendRequest friendRequest)
        {
            _context.FriendRequests.Update(friendRequest);
        }
    }
}
