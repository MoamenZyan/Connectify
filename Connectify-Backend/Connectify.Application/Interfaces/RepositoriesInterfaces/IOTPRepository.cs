using Connectify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.Interfaces.RepositoriesInterfaces
{
    public interface IOTPRepository : IRepository<OTP>
    {
        void UpdateOTPAsync(OTP otp);
        Task DeleteOTPAsync(Guid otpId);
    }
}
