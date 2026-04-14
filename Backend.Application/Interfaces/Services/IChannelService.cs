using Backend.Application.DTO.Channel;
using Backend.Application.DTO.Service;
using Backend.Domain.Models;

namespace Backend.Application.Interfaces.Services;
public interface IChannelService
{
    Task<ServiceDataResult<List<ChannelSummary>>> GetChannelSummaryAsync();
    Task<ServiceDataResult<Channel>> AddChannelAsync(ChannelRequest request);
    Task<ServiceResult> EditChannelAsync(Guid channelId, ChannelRequest request);
    Task<ServiceResult> DeleteChannelAsync(Guid channelId);
}

