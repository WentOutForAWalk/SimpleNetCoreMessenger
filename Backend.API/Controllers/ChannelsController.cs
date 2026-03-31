using Backend.API.DTO.Channel;
using Backend.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        var newChannel = channelService.AddChannel(request);
        // [LOG]
        Console.WriteLine($"[LOG]: User<{User.Identity?.Name}> CREATE channel<{newChannel.ChannelName}> id<{newChannel.ChannelId}>");
        return Created($"a/channels/{newChannel.ChannelId}", newChannel);
    }


    [HttpPut("{сhannelId:guid}")]
    public IActionResult Update(Guid сhannelId, [FromBody] ChannelRequest request)
    {
        if (channelService.EditChannel(сhannelId, request))
        {
            // [LOG]
            Console.WriteLine($"[LOG]: User<{User.Identity?.Name}> UPDATE channel by id<{сhannelId}>");
            return NoContent();
        }
        return NotFound();
    }


    [HttpDelete("{сhannelId:guid}")]
    public IActionResult Delete(Guid сhannelId) {
        if (channelService.DeleteChannel(сhannelId))
        {
            Console.WriteLine($"[LOG]: User<{User.Identity?.Name}> DELETE channel by id<{сhannelId}>");
            return NoContent();
        }
        return NotFound();
    }
    







}
