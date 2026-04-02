using Backend.API.DTO.Message;
using Backend.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Backend.API.Controllers;


[Authorize]
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
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userName = User.Identity?.Name;

        if (_messageService.AddMessage(channelId, request, userId, userName) is { } message)
        {
            return Created($"a/channels/{channelId}/messages/{message.MessageId}", message);
        }
        return NotFound();
    }


    [HttpPut("{messageId:guid}")]
    public IActionResult Update(Guid channelId, Guid messageId, CreateMessageRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userName = User.Identity?.Name;

        if (_messageService.EditMessage(userId, userName, channelId, messageId, request))
        {
            return Ok();
        }
        return NotFound();
    }


    [HttpDelete("{messageId:guid}")]
    public IActionResult Delete(Guid channelId, Guid messageId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (_messageService.RemoveMessage(channelId, messageId, userId))
        {
            return Ok();
        }
        return NotFound();
    }

}