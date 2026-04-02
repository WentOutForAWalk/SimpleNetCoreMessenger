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

    public Message? AddMessage(Guid channelId, CreateMessageRequest request, string userId, string userName)
    {

        if (_context.Channels.FirstOrDefault(c => c.ChannelId == channelId) is not { })
            return null;

        Message message = new()
        {
            SenderName = userName,
            Text = request.Text,
            ChannelId = channelId,
            OwnerId = userId
        };
        _context.Messages.Add(message);
        _context.SaveChanges();
        return message;
    }

    public bool EditMessage(string userId, string userName, Guid channelId, Guid messageId, CreateMessageRequest request)
    {
        if (_context.Messages.FirstOrDefault(m => m.OwnerId == userId && m.MessageId == messageId && m.ChannelId == channelId) is not { } message)
            return false;

        message.Text = request.Text;
        message.SenderName = userName;
        
        _context.SaveChanges();
        return true;
    }

    // wow
    public bool RemoveMessage(Guid channelId, Guid messageId, string userId)
    {

        var message = new Message
        {
            OwnerId = userId,
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

