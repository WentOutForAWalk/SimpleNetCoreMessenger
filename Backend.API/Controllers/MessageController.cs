using Microsoft.AspNetCore.Mvc;
using Backend.API.DTO.Message;
using Backend.API.Services;

namespace Backend.API.Controllers;



[ApiController]
[Route("a/channels/{channelId:guid}/messages")]
public class MessageController : ControllerBase
{
    MessageService _messageService;

    public MessageController(MessageService messageService)
    {
        _messageService = messageService;
    }


    [HttpGet]
    public IActionResult Read(Guid channelId)
    {
        if (_messageService.GetChannelMessage(channelId) is { } channelMessage)
        {
            return Ok(channelMessage);
        }
        return NotFound();
    }


    [HttpPost]
    public IActionResult Create(Guid channelId, CreateMessageRequest request)
    {
        if (_messageService.AddMessage(channelId, request) is { } message)
        {
            return Created($"a/channels/{channelId}/messages/{message.MessageId}", message);
        }
        return NotFound();
    }


    [HttpPut("{messageId:guid}")]
    public IActionResult Update(Guid channelId, Guid messageId, CreateMessageRequest request)
    {
        if (_messageService.EditMessage(channelId, messageId, request))
        {
            return Ok();
        }
        return NotFound();
    }


    [HttpDelete("{messageId:guid}")]
    public IActionResult Delete(Guid channelId, Guid messageId)
    {
        if (_messageService.RemoveMessage(channelId, messageId))
        {
            return Ok();
        }
        return NotFound();
    }

}