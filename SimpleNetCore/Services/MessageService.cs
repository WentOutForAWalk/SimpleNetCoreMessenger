using SimpleNetCore.DTO.Channel;
using SimpleNetCore.DTO.Message;
using System.Reflection.Metadata.Ecma335;

namespace SimpleNetCore.Services;
public class MessageService
{
    MemoryStorageService _storage;
    public MessageService(MemoryStorageService storage)
    {
        _storage = storage;
    }

    public ChannelDto? GetChannelMessage(Guid id)
    {
        return _storage.Channels.FirstOrDefault(c => c.ChannelId == id);
    }

    public MessageDto? AddMessage(Guid id, CreateMessageRequest request)
    {

        if (_storage.Channels.FirstOrDefault(c => c.ChannelId == id) is not { } channel)
            return null;

        MessageDto messageDto = new MessageDto
        {
            SenderName = request.SenderName,
            Text = request.Text,
        };
        channel.Messages.Add(messageDto);
        return messageDto;
    }

    public bool EditMessage(Guid channelId, Guid messageId, CreateMessageRequest request)
    {
        if (_storage.Channels.FirstOrDefault(c => c.ChannelId == channelId) is not { } channel)
            return false;

        if (channel.Messages.FindIndex(c => c.MessageId == messageId) is var index and not -1)
        {
            channel.Messages[index].SenderName = request.SenderName;
            channel.Messages[index].Text = request.Text;

            return true;
        }
        return false;
    }

    public bool RemoveMessage(Guid channelId, Guid messageId)
    {
        if (_storage.Channels.FirstOrDefault(c => c.ChannelId == channelId) is not { } channel)
            return false;

        if (channel.Messages.FindIndex(c => c.MessageId == messageId) is var index and not -1)
        {
            channel.Messages.RemoveAt(index);
            return true;
        }
        return false;
    }
}

