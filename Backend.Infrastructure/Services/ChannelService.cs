using Backend.Application.DTO.Channel;
using Backend.Application.DTO.Service;
using Backend.Application.Interfaces.Services;
using Backend.Domain.Models;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Services;

public class ChannelService : IChannelService
{
    private readonly DataContext _context;
    private readonly IUserContextService _userContext;
    public ChannelService(DataContext context, IUserContextService userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<ServiceDataResult<List<ChannelSummary>>> GetChannelSummaryAsync()
    {
        var data = await _context.Channels.Select(c => new ChannelSummary(c.ChannelId, c.ChannelName, c.OwnerId)).ToListAsync();
        return ServiceDataResult<List<ChannelSummary>>.SuccessWithDate(data);
    }
    public async Task<ServiceDataResult<Channel>> AddChannelAsync(ChannelRequest request)
    {
        Channel newChannel = new()
        {
            ChannelName = request.ChannelName,
            OwnerId = _userContext.GetUserId()
        };

        _context.Channels.Add(newChannel);
        await _context.SaveChangesAsync();

        return ServiceDataResult<Channel>.SuccessWithDate(newChannel);
    }
    public async Task<ServiceResult> EditChannelAsync(Guid channelId, ChannelRequest request)
    {
        if (_context.Channels.FirstOrDefault(c => c.ChannelId == channelId && c.OwnerId == _userContext.GetUserId()) is not { } channel)
            return ServiceResult.Failure("no permission");
            
        channel.ChannelName = request.ChannelName;
        await _context.SaveChangesAsync();
        return ServiceResult.Success();
    }
    public async Task<ServiceResult> DeleteChannelAsync(Guid id)
    {
        if (_context.Channels.FirstOrDefault(c => c.ChannelId == id && c.OwnerId == _userContext.GetUserId()) is not { } channel)
            return ServiceResult.Failure("no permission");

        _context.Channels.Remove(channel);
        await _context.SaveChangesAsync();

        return ServiceResult.Success();
    }
}
