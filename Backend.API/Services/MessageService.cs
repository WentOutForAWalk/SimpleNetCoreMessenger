using Backend.API.Data;
using Backend.API.DTO.Message;
using Backend.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Services;
public class MessageService
{
    private readonly DataContext _context;
    public MessageService(DataContext context)
    {
        _context = context;
    }

    public Channel? GetChannelMessage(Guid id)
    {
        return _context.Channels.Include(c => c.Messages)
                                .FirstOrDefault(c => c.ChannelId == id);
    }

    public Message? AddMessage(Guid channelId, CreateMessageRequest request)
    {

        if (_context.Channels.FirstOrDefault(c => c.ChannelId == channelId) is not { })
            return null;

        Message message = new Message
        {
            SenderName = request.SenderName,
            Text = request.Text,
            ChannelId = channelId,
        };
        _context.Messages.Add(message);
        _context.SaveChanges();
        return message;
    }

    public bool EditMessage(Guid channelId, Guid messageId, CreateMessageRequest request)
    {
        if (_context.Messages.FirstOrDefault(m => m.MessageId == messageId) is not { } message)
            return false;

        if (message.ChannelId != channelId) 
        {
            return false;
        }

        message.Text = request.Text;
        message.SenderName = request.SenderName;
        
        _context.SaveChanges();
        return true;
    }

    // wow
    public bool RemoveMessage(Guid channelId, Guid messageId)
    {

        var message = new Message
        {
            ChannelId = channelId,
            MessageId = messageId
        };

        _context.Entry(message).State = EntityState.Deleted;

        try
        {
            return _context.SaveChanges() > 0;
        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }

    }
}

