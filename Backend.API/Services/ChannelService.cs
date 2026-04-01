using Backend.API.Data;
using Backend.API.DTO.Channel;
using Backend.API.Models;

namespace Backend.API.Services;

public class ChannelService
{
    private readonly DataContext _context;
    public ChannelService(DataContext context)
    {
        _context = context;
    }

    public IEnumerable<ChannelSummary> GetChannelSummary()
    {
        return (_context.Channels.Select(c => new ChannelSummary(c.ChannelId, c.ChannelName, c.OwnerId)));
    }
    public Channel AddChannel(ChannelRequest request, string userId)
    {
        Channel newChannel = new Channel
        {
            ChannelName = request.ChannelName,
            OwnerId = userId
        };

        _context.Channels.Add(newChannel);
        _context.SaveChanges();


        return newChannel;
    }
    public bool EditChannel(Guid id, ChannelRequest request)
    {
        if (_context.Channels.FirstOrDefault(c => c.ChannelId == id) is not { } channel)
            return false;
        channel.ChannelName = request.ChannelName;
        _context.SaveChanges();
        return true;
    }
    public bool DeleteChannel(Guid id)
    {
        if (_context.Channels.FirstOrDefault(c => c.ChannelId == id) is not { } channel)
        {
            return false;
        }

        _context.Channels.Remove(channel);
        _context.SaveChanges();

        return true;
    }


}
