using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.Interfaces.HubInterfaces
{
    public interface IChatHub
    {
        Task UserIsTyping(string chatId);
        Task UserStoppedTyping(string chatId);
        Task UserSeenMessages(string receiverGuid, string[] messagesGuid);
        Task SendMessageToSpecificUser(string message, string receiverGuid, string messageGuid, string messageAttachmentURL);
    }
}
