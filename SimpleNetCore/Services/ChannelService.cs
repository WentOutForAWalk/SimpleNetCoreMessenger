using SimpleNetCore.DTO.Channel;

namespace SimpleNetCore.Services;

public class ChannelService
{
    private readonly MemoryStorageService _storage;
    public ChannelService(MemoryStorageService storage)
    {
        _storage = storage;
    }

    public IEnumerable<ChannelSummary> GetChannelSummary()
    {
        return (_storage.Channels.Select(c => new ChannelSummary(c.ChannelId, c.ChannelName)));
    }


    // Adds a Channel in memory storage <temporary solution>
    public ChannelDto AddChannel(ChannelRequest request)
    {
        ChannelDto newChannel = new ChannelDto
        {
            ChannelName = request.ChannelName
        };

        _storage.Channels.Add(newChannel);
        
        return newChannel;
    }

    public bool EditChannel(Guid id, ChannelRequest request)
    {
        if (_storage.Channels.FirstOrDefault(c => c.ChannelId == id) is not { } channel)
            return false;

        channel.ChannelName = request.ChannelName;
        return true;
    }

    public bool DeleteChannel(Guid id)
    {
        if (_storage.Channels.FindIndex(c => c.ChannelId == id) is var index and not -1)
        {
            _storage.Channels.RemoveAt(index);
            return true;
        }
        return false;
    }


}
