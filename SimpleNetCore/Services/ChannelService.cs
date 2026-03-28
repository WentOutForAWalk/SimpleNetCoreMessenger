using SimpleNetCore.DTO.Channel;

namespace SimpleNetCore.Services;

public class ChannelService
{
    private readonly MemoryStorageService Storage;

    public ChannelService(MemoryStorageService Storage)
    {
        this.Storage = Storage;
    }

    // Adds a Channel in memory storage <temporary solution>
    public ChannelDto AddChannel(ChannelRequest request)
    {
        ChannelDto newChannel = new ChannelDto
        {
            ChannelName = request.ChannelName
        };

        Storage.Channels.Add(newChannel);
        
        return newChannel;
    }

    public IEnumerable<ChannelSummary> GetChannelSummary()
    {
        return (Storage.Channels.Select(c => new ChannelSummary(c.ChannelId, c.ChannelName)));
    }

    public bool EditChannel(Guid id, ChannelRequest request)
    {
        if (Storage.Channels.FirstOrDefault(c => c.ChannelId == id) is not { } channel)
            return false;

        channel.ChannelName = request.ChannelName;
        return true;
    }

}
