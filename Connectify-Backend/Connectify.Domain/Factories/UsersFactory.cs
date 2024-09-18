using Connectify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ganss.Xss;
using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using System.Numerics;

namespace Connectify.Domain.Factories
{
    public class UsersFactory
    {
        static HtmlSanitizer sanitizer = new HtmlSanitizer();
        public static User CreateUser(IFormCollection data)
        {
            var user = new User();
            user.Id = Guid.NewGuid();
            if (data.ContainsKey("Fname"))
                user.Fname = sanitizer.Sanitize(Convert.ToString(data["Fname"]));

            if (data.ContainsKey("Lname"))
                user.Lname = sanitizer.Sanitize(Convert.ToString(data["Lname"]));

            if (data.ContainsKey("Email"))
                user.Email = sanitizer.Sanitize(Convert.ToString(data["Email"]));

            if (data.ContainsKey("Phone"))
                user.Phone = sanitizer.Sanitize(Convert.ToString(data["Phone"]));

            if (data.ContainsKey("Password"))
                user.Password = BCrypt.Net.BCrypt.HashPassword(Convert.ToString(data["Password"]), 10);

            return user;
        }
    }
}
