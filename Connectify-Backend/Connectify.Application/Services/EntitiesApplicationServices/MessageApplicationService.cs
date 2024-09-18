using Connectify.Application.DTOs;
using Connectify.Application.Interfaces.ApplicationServicesInterfaces;
using Connectify.Application.Interfaces.RepositoriesInterfaces;
using Connectify.Domain.Entities;
using Connectify.Domain.Enums;
using Connectify.Domain.Factories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.Services.EntitiesApplicationServices
{
    public class MessageApplicationService : IMessageApplicationService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUnitOfWork _unitOfWork;
        public MessageApplicationService(IMessageRepository messageRepository, IUnitOfWork unitOfWork)
        {
            _messageRepository = messageRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Message> CreateMessage(Guid senderId, Guid chatId, string content, string attachmentUrl, Guid messageGuid, MessageStatus status)
        {
            Message message = MessagesFactory.CreateMessage(senderId, chatId, content, attachmentUrl, messageGuid, status);
            await _messageRepository.AddAsync(message);
            await _unitOfWork.SaveChangesAsync();

            return message;
        }

        public Task<bool> DeleteMessage(int messageId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateMessage(int messageId, string content)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ViewMessage(int messageId, int viewerId)
        {
            throw new NotImplementedException();
        }
    }
}
