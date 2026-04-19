using Backend.Application.DTO.Message;
using Backend.Application.DTO.Service;
using Backend.Domain.Models;

namespace Backend.Application.Interfaces.Services;

public interface IMessageService
{
    Task<ServiceDataResult<List<Message>>> GetChannelMessageAsync(Guid id);
    Task<ServiceDataResult<Message>> AddMessageAsync(Guid channelId, CreateMessageRequest request);
    Task<ServiceResult> EditMessageAsync(Guid messageId, CreateMessageRequest request);
    Task<ServiceResult> RemoveMessageAsync(Guid messageId);
}

