using Backend.API.DTO.Message;
using Backend.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Backend.API.Extensions;

namespace Backend.API.Controllers;


[Authorize]
[ApiController]
[Route("a/messages")]
public class MessageController : ControllerBase
{
    private readonly MessageService _messageService;

    public MessageController(MessageService messageService)
    {
        _messageService = messageService;
    }


    [HttpGet]
    public async Task<IActionResult> ReadAsync([FromQuery] Guid channelId)
    {
        var result = await _messageService.GetChannelMessageAsync(channelId);
        return result.ToActionResult();
    }
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromQuery] Guid channelId, [FromBody] CreateMessageRequest request)
    {
        var result = await _messageService.AddMessageAsync(channelId, request);
        return result.ToActionResult();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromQuery] Guid messageId, [FromBody] CreateMessageRequest request)
    {
        var result = await _messageService.EditMessageAsync(messageId, request);
        return result.ToActionResult();
    }


    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromQuery]  Guid messageId)
    {
        var result = await _messageService.RemoveMessageAsync(messageId);
        return result.ToActionResult();
    }

}