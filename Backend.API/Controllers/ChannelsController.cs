using Backend.API.DTO.Channel;
using Backend.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Backend.API.Controllers;


[Authorize]
[ApiController]
[Route("a/channels")]
public class ChannelsController : ControllerBase
{
    ChannelService channelService;

    public ChannelsController(ChannelService channelService)
    {
        this.channelService = channelService;
    }


    [HttpGet]
    public IActionResult Read()
    {   // channel summary
        return Ok(channelService.GetChannelSummary());
    }


    [HttpPost]
    public IActionResult Create([FromBody] ChannelRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var newChannel = channelService.AddChannel(request, userId);
        return Created($"a/channels/{newChannel.ChannelId}", newChannel);
    }


    [HttpPut("{сhannelId:guid}")]
    public IActionResult Update(Guid сhannelId, [FromBody] ChannelRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (channelService.EditChannel(сhannelId, request, userId))
        {
            return NoContent();
        }
        return NotFound();
    }


    [HttpDelete("{сhannelId:guid}")]
    public IActionResult Delete(Guid сhannelId) 
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (channelService.DeleteChannel(сhannelId, userId))
        {
            return NoContent();
        }
        return NotFound();
    }
    







}
