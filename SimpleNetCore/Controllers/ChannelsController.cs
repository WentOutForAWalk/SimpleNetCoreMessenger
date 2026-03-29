using Microsoft.AspNetCore.Mvc;
using SimpleNetCore.DTO.Channel;
using SimpleNetCore.Services;

namespace SimpleNetCore.Controllers;



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

        return Created($"a/channels/{newChannel.ChannelId}", newChannel);
    }


    [HttpPut("{сhannelId:guid}")]
    public IActionResult Update(Guid сhannelId, [FromBody] ChannelRequest request)
    {
        if (channelService.EditChannel(сhannelId, request))
        {
            return NoContent();
        }
        return NotFound();
    }


    [HttpDelete("{сhannelId:guid}")]
    public IActionResult Delete(Guid сhannelId) {
        if (channelService.DeleteChannel(сhannelId))
        {
            return NoContent();
        }
        return NotFound();
    }
    







}
