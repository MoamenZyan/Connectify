using Connectify.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Domain.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUser(IFormCollection data, Func<string, Task<User?>> checkUserEmail, Func<string, Task<User?>> checkUserPhone);
        void UpdateUser(User originalUser, User updatedUser);
        void ValidateFname(string firstName);
        void ValidateLname(string lastName);
        Task ValidateEmail(string email, Func<string, Task<User?>> checkUserEmail);
        void ValidatePassword(string password);
        Task ValidatePhoneNumber(string phoneNumber, Func<string, Task<User?>> checkUserPhone);
        Task<(bool, string?)> ValidateUserCredentials(UserCredentials userCredentials, Func<string, Task<User?>> func);
    }
}
