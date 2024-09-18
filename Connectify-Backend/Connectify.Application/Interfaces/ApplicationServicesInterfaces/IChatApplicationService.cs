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
    public interface IChatApplicationService
    {
        Task<(bool, Guid)> CreateNormalChat(Guid currentUserId, Guid chatUserId);
        Task<(bool, Guid)> CreateGroupChat(IFormCollection chatData, Guid currentUserId);
        Task<ChatDto?> GetPrivateChat(Guid currentUserId, Guid receiverId);
        Task<bool> DeleteChat(int chatId);
        Task<bool> UserJoinGroupChat(int chatId, int userId, UserRole role);
        Task<bool> UserLeaveGroupChat(int chatId, int userId);
    }
}
