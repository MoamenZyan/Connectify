using Connectify.Domain.Entities;
using Connectify.Domain.Factories;
using Connectify.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Connectify.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly string nameRegexPattern = @"^[^\d][a-zA-Z0-9_]{0,39}$";
        private readonly string emailRegexPattern = @"^[^\s@]+@[^\s@]+\.com+$";
        private readonly string phoneRegexPattern = @"^(012|010|015|011)\d{8}$";
        public async Task<User> CreateUser(IFormCollection data, Func<string, Task<User?>> checkUserEmail, Func<string, Task<User?>> checkUserPhone)
        {
            Regex phoneRegex = new Regex(phoneRegexPattern);
            Regex nameRegex = new Regex(nameRegexPattern);
            Regex emailRegex = new Regex(emailRegexPattern);


            if (data == null) 
                throw new ArgumentNullException("data is null");

            if (data.ContainsKey("Fname"))
            {
                if (Convert.ToString(data["Fname"]) == null || Convert.ToString(data["Fname"]).Length == 0)
                    throw new Exception("Enter first name");

                if (!nameRegex.IsMatch(data["Fname"]))
                    throw new Exception("First name must don't exceed 40 character and doesn't start with a number or have spaces");
            }

            if (data.ContainsKey("Lname"))
            {
                if (Convert.ToString(data["Lname"]) == null || Convert.ToString(data["Lname"]).Length == 0)
                    throw new Exception("Enter last name");

                if (!nameRegex.IsMatch(data["Lname"]))
                    throw new Exception("Last name must don't exceed 40 character and doesn't start with a number or have spaces");
            }

            if (data.ContainsKey("Email"))
            {
                if (Convert.ToString(data["Email"]) == null || Convert.ToString(data["Email"]).Length == 0)
                    throw new Exception("Enter email");

                if ((await checkUserEmail(data["Email"])) != null)
                    throw new Exception("Email already exists");

                if (!emailRegex.IsMatch(data["Email"]))
                    throw new Exception("Incorrect email");
            }

            if (data.ContainsKey("Password"))
            {
                if (Convert.ToString(data["Password"]) == null || Convert.ToString(data["Password"]).Length < 10)
                    throw new Exception("Password must be over 10 characters and digits");
            }

            if (data.ContainsKey("Phone"))
            {
                if (Convert.ToString(data["Phone"]) == null || Convert.ToString(data["Phone"]).Length == 0)
                    throw new Exception("Enter phone");

                if ((await checkUserPhone(data["Phone"])) != null)
                    throw new Exception("Phone already exists");

                if (!phoneRegex.IsMatch(data["Phone"]))
                    throw new Exception("Phone must be in egypt");
            }

            return UsersFactory.CreateUser(data);
        }

        public void UpdateUser(User originalUser, User updatedUser)
        {
            if (updatedUser.Fname != null)
                originalUser.Fname = updatedUser.Fname;

            if (updatedUser.Lname != null)
                originalUser.Lname = updatedUser.Lname;

            if (updatedUser.Email != null)
                originalUser.Email = updatedUser.Email;

            if (updatedUser.Password != null)
                originalUser.Password = updatedUser.Password;

            if (updatedUser.Phone != null)
                originalUser.Phone = updatedUser.Phone;
        }

        public async Task ValidateEmail(string email, Func<string, Task<User?>> checkUserEmail)
        {
            Regex emailRegex = new Regex(emailRegexPattern);

            if (email == null || email.Length == 0)
                throw new Exception("Enter email");

            if ((await checkUserEmail(email)) != null)
                throw new Exception("Email already exists");

            if (!emailRegex.IsMatch(email))
                throw new Exception("Incorrect email");

            return;
        }

        public void ValidateFname(string firstName)
        {
            Regex nameRegex = new Regex(nameRegexPattern);

            if (firstName == null || firstName.Length == 0)
                throw new Exception("Enter first name");

            if (!nameRegex.IsMatch(firstName))
                throw new Exception("First name must don't exceed 40 character and doesn't start with a number or have spaces");

            return;
        }

        public void ValidateLname(string lastName)
        {
            Regex nameRegex = new Regex(nameRegexPattern);

            if (lastName == null || lastName.Length == 0)
                throw new Exception("Enter last name");

            if (!nameRegex.IsMatch(lastName))
                throw new Exception("Last name must don't exceed 40 character and doesn't start with a number or have spaces");

            return;
        }

        public void ValidatePassword(string password)
        {
            if (password == null || password.Length < 10)
                throw new Exception("Password must be over 10 characters and digits");

            return;
        }

        public async Task ValidatePhoneNumber(string phoneNumber, Func<string, Task<User?>> checkUserPhone)
        {
            Regex phoneRegex = new Regex(phoneRegexPattern);

            if (phoneNumber == null || phoneNumber.Length == 0)
                throw new Exception("Enter phone");

            if ((await checkUserPhone(phoneNumber)) != null)
                throw new Exception("Phone already exists");

            if (!phoneRegex.IsMatch(phoneNumber))
                throw new Exception("Phone must be in egypt");

            return;
        }

        public async Task<(bool, string?)> ValidateUserCredentials(UserCredentials userCredentials, Func<string, Task<User?>> func)
        {
            var user = await func(userCredentials.Email.ToLower());
            if (user == null)
                return (false, null);

            if (BCrypt.Net.BCrypt.Verify(userCredentials.Password, user.Password))
                return (true, Convert.ToString(user.Id));
            return (false, null);
        }
    }
}
