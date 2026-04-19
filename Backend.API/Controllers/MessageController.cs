using Backend.API.Extensions;
using Backend.Application.DTO.Message;
using Backend.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;


[Authorize]
[ApiController]
[Route("a/messages")]
public class MessageController : ControllerBase
{
    private readonly IMessageService _messageService;

    public MessageController(IMessageService messageService)
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