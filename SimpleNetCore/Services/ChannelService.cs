using SimpleNetCore.DTO.Channel;

namespace SimpleNetCore.Services;

public class ChannelService
{
    private readonly MemoryStorageService Storage;

    public ChannelService(MemoryStorageService Storage)
    {
        this.Storage = Storage;
    }

    public IEnumerable<ChannelSummary> GetChannelSummary()
    {
        return (Storage.Channels.Select(c => new ChannelSummary(c.ChannelId, c.ChannelName)));
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

    public bool EditChannel(Guid id, ChannelRequest request)
    {
        if (Storage.Channels.FirstOrDefault(c => c.ChannelId == id) is not { } channel)
            return false;

        channel.ChannelName = request.ChannelName;
        return true;
    }

    public bool DeleteChannel(Guid id)
    {
        if (Storage.Channels.FindIndex(c => c.ChannelId == id) is var index and not -1)
        {
            Storage.Channels.RemoveAt(index);
            return true;
        }
        return false;
    }


}
