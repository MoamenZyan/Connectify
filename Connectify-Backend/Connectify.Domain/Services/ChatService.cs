using Connectify.Domain.Entities;
using Connectify.Domain.Enums;
using Connectify.Domain.Factories;
using Connectify.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;


namespace Connectify.Domain.Services
{
    public class ChatService : IChatService
    {
        public Chat CreateChat(ChatType chatType, string photo, IFormCollection data = null!)
        {
            if (data == null && chatType == ChatType.Group)
                throw new ArgumentNullException("chat data is null");

            try
            {
                if (chatType == ChatType.Group)
                    ValidateChatName(data!["Name"]);
            }
            catch (Exception)
            {
                throw;
            }
            Chat chat = ChatsFactory.CreateChat(data!, chatType, photo);
            return chat;
        }

        public void ValidateChatName(string chatName)
        {
            var pattern = @"^[^\d][a-zA-Z0-9_ ]*$";
            Regex chatNameRegex = new Regex(pattern);

            if (chatName.Length == 0)
                throw new ArgumentNullException("chat name doesn't exist");

            if (!chatNameRegex.IsMatch(chatName))
                throw new Exception("chat name isn't correct");

            return;
        }
    }
}
