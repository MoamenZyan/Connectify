using Connectify.Application.DTOs;
using Connectify.Domain.Entities;
using Connectify.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.Interfaces.ApplicationServicesInterfaces
{
    public interface IMessageApplicationService
    {
        Task<Message> CreateMessage(Guid senderId, Guid chatId, string content, string attachmentUrl, Guid messageGuid, MessageStatus status);
        Task<bool> DeleteMessage(int messageId);
        Task<bool> UpdateMessage(int messageId, string content);
        Task<bool> ViewMessage(int messageId, int viewerId);
    }
}
