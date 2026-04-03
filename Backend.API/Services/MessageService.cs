using Backend.API.Data;
using Backend.API.DTO.Message;
using Backend.API.DTO.Service;
using Backend.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Services;
public class MessageService
{
    private readonly DataContext _context;
    private readonly UserContextService _userContext;
    public MessageService(DataContext context,UserContextService userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<ServiceDataResult<List<Message>>> GetChannelMessageAsync(Guid id)
    {
        return ServiceDataResult<List<Message>>.SuccessWithDate(await _context.Messages.Where(m => m.ChannelId == id).ToListAsync());
    }
    public async Task<ServiceDataResult<Message>> AddMessageAsync(Guid channelId, CreateMessageRequest request)
    {

        if (!await _context.Channels.AnyAsync(c => c.ChannelId == channelId))
            return ServiceDataResult<Message>.Failure("Channel not found");

        Message message = new()
        {
            SenderName = _userContext.GetUserName(),
            Text = request.Text,
            ChannelId = channelId,
            OwnerId = _userContext.GetUserId()
        };
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();
        return ServiceDataResult<Message>.Success();
    }
    public async Task<ServiceResult> EditMessageAsync(Guid messageId, CreateMessageRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Text))
            return ServiceResult.Failure("empty text");

        if (await _context.Messages.FirstOrDefaultAsync(m => m.MessageId == messageId 
                                                          && m.OwnerId == _userContext.GetUserId()
                                                       ) is not { } message)
        return ServiceResult.Failure("no permission");

        message.Text = request.Text;
        await _context.SaveChangesAsync();

        return ServiceResult.Success();
    }
    public async Task<ServiceResult> RemoveMessageAsync(Guid messageId)
    {

        var message = new Message
        {
            MessageId = messageId,
            OwnerId = _userContext.GetUserId()
        };

        _context.Entry(message).State = EntityState.Deleted;

        try
        {
            if (await _context.SaveChangesAsync() > 0)
                return ServiceResult.Success();
            return ServiceResult.Failure("db save changes error");
        }
        catch (DbUpdateConcurrencyException)
        {
            return ServiceResult.Failure("no permission");
        }

    }
}

