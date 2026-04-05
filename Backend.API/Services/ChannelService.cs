using Backend.API.Data;
using Backend.API.DTO.Channel;
using Backend.API.DTO.Service;
using Backend.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Services;

public class ChannelService
{
    private readonly DataContext _context;
    private readonly UserContextService _userContext;
    public ChannelService(DataContext context, UserContextService userContext)
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
