using SimpleNetCore.Data;
using SimpleNetCore.DTO.Channel;
using SimpleNetCore.Models;

namespace SimpleNetCore.Services;

public class ChannelService
{
    private readonly MemoryStorageService _storage;
    private readonly DataContext _context;
    public ChannelService(MemoryStorageService storage, DataContext context)
    {
        _storage = storage;
        _context = context;
    }

    public IEnumerable<ChannelSummary> GetChannelSummary()
    {
        return (_context.Channels.Select(c => new ChannelSummary(c.ChannelId, c.ChannelName)));
    }


    // Adds a Channel in memory storage <temporary solution>
    public Channel AddChannel(ChannelRequest request)
    {
        Channel newChannel = new Channel
        {
            ChannelName = request.ChannelName
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
