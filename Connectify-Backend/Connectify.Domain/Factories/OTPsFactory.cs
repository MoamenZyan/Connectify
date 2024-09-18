using Connectify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Domain.Factories
{
    public class OTPsFactory
    {
        public static OTP CreateOTP(Guid userId)
        {
            OTP otp = new OTP()
            {
                Id = Guid.NewGuid(),
                Number = GenerateOTPNumber(),
                UserId = userId,
                Deadline = DateTime.Now.AddMinutes(6)
            };
            return otp;
        }

        private static string GenerateOTPNumber()
        {
            Random random = new Random();
            random.Next(100000, 999999999);
            return random.Next(100000, 999999999).ToString().Substring(0, 6);
        }
    }
}
