using Backend.API.Extensions;
using Backend.Application.DTO.Channel;
using Backend.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;


[Authorize]
[ApiController]
[Route("a/channels")]
public class ChannelsController : ControllerBase
{
    private readonly IChannelService _channelService;

    public ChannelsController(IChannelService channelService)
    {
        _channelService = channelService;
    }

    [HttpGet]
    public async Task<IActionResult> ReadAsync()
    {   // channel summary
        var result = await _channelService.GetChannelSummaryAsync();
        return result.ToActionResult();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] ChannelRequest request)
    {
        var result = await _channelService.AddChannelAsync(request);
        return result.ToActionResult();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromQuery] Guid сhannelId, [FromBody] ChannelRequest request)
    {
        var result = await _channelService.EditChannelAsync(сhannelId, request);
        return result.ToActionResult();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromQuery] Guid сhannelId) 
    {
        var result = await _channelService.DeleteChannelAsync(сhannelId);
        return result.ToActionResult();
    }
}
