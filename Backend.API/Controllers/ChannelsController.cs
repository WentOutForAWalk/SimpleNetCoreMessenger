using Backend.API.Extensions;
using Backend.Application.DTO.Channel;
using Backend.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;


[Authorize]
[ApiController]
[Route("a/channels")]
public class ChannelsController : ControllerBase
{
    private readonly ChannelService channelService;

    public ChannelsController(ChannelService channelService)
    {
        this.channelService = channelService;
    }

    [HttpGet]
    public async Task<IActionResult> ReadAsync()
    {   // channel summary
        var result = await channelService.GetChannelSummaryAsync();
        return result.ToActionResult();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] ChannelRequest request)
    {
        var result = await channelService.AddChannelAsync(request);
        return result.ToActionResult();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromQuery] Guid сhannelId, [FromBody] ChannelRequest request)
    {
        var result = await channelService.EditChannelAsync(сhannelId, request);
        return result.ToActionResult();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromQuery] Guid сhannelId) 
    {
        var result = await channelService.DeleteChannelAsync(сhannelId);
        return result.ToActionResult();
    }
}
