using Connectify.Domain.Entities;
using Connectify.Domain.Enums;
using Ganss.Xss;
using Microsoft.AspNetCore.Http;

namespace Connectify.Domain.Factories
{
    public class ChatsFactory
    {
        static HtmlSanitizer sanitizer = new HtmlSanitizer();
        public static Chat CreateChat(IFormCollection data, ChatType chatType, string photo)
        {
            Chat chat = new Chat()
            {
                Id = Guid.NewGuid(),
                Name = chatType == ChatType.Normal ? "" : sanitizer.Sanitize(data["Name"]),
                Description = chatType == ChatType.Normal ? "" : sanitizer.Sanitize(data["Description"]),
                CreatedAt = DateTime.Now,
                Type = chatType,
                Photo = photo
            };
            return chat;
        }
    }
}
