using SimpleNetCore.DTO.Channel;
using SimpleNetCore.DTO.Message;

namespace SimpleNetCore.Services;
public class MemoryStorageService
{
    public List<ChannelDto> Channels { get; } = new List<ChannelDto>();


    public MemoryStorageService() 
    {
        Channels.Add(new ChannelDto
        {
            ChannelName = "WentOutForAWalk",

            Messages = new List<MessageDto>
        {
            new MessageDto
            {
                SenderName = "WentOutForAWalk",
                Text = "Hello World!",
            }
        }
        });
        Channels.Add(new ChannelDto
        {
            ChannelName = "Insider UA",
        });
    }



}
