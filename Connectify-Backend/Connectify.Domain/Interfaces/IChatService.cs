using Connectify.Domain.Entities;
using Connectify.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Domain.Interfaces
{
    public interface IChatService
    {
        void ValidateChatName(string chatName);
        Chat CreateChat(ChatType chatType, string photo, IFormCollection data = null!);

    }
}
