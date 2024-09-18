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
    public class OTPRepository : IOTPRepository
    {
        private readonly AppDbContext _context;
        public OTPRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(OTP item)
        {
            await _context.OTPs.AddAsync(item);
        }

        public async Task DeleteOTPAsync(Guid otpId)
        {
            await _context.OTPs.Where(x => x.Id == otpId).ExecuteDeleteAsync();
        }

        public IEnumerable<OTP> Filter(Func<OTP, bool> predicate)
        {
            return _context.OTPs.Where(predicate);
        }

        public async Task<IEnumerable<OTP>> GetAllAsync()
        {
            return await _context.OTPs.ToListAsync();
        }

        public void UpdateOTPAsync(OTP otp)
        {
            _context.OTPs.Update(otp);
        }
    }
}
