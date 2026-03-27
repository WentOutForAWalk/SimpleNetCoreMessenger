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
    public ChannelDto AddChannel(string ChannelName)
    {
        ChannelDto newChannel = new ChannelDto
        {
            ChannelName = ChannelName
        };

        Storage.Channels.Add(newChannel);
        
        return newChannel;
    }


}
