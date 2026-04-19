using Backend.Application.DTO.Message;
using Backend.Application.DTO.Service;
using Backend.Application.Interfaces.Services;
using Backend.Domain.Models;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Services;
public class MessageService : IMessageService
{
    private readonly DataContext _context;
    private readonly IUserContextService _userContext;
    public MessageService(DataContext context, IUserContextService userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<ServiceDataResult<List<Message>>> GetChannelMessageAsync(Guid id)
    {
        var messagesList = await _context.Messages.Where(m => m.ChannelId == id).ToListAsync();
        if (messagesList.Count == 0)
        {
            return ServiceDataResult<List<Message>>.Failure("Channel not found");
        }
        return ServiceDataResult<List<Message>>.SuccessWithDate(messagesList);
        
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
        await _context.Messages.AddAsync(message);
        await _context.SaveChangesAsync();
        return ServiceDataResult<Message>.SuccessWithDate(message);
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

