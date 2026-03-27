using Microsoft.AspNetCore.Mvc;
using SimpleNetCore.DTO.Channel;

namespace SimpleNetCore.Controllers;



[ApiController]
[Route("a/channels")]
public class ChannelsController : ControllerBase
{
    private List<ChannelDto> Channels;

    ChannelsController(List<ChannelDto> Channels)
    {
        this.Channels = Channels;
    }



    [HttpGet]
    public IActionResult GetAll()
    {
        var summary = Channels.Select(c => new ChannelSummary(c.ChannelId, c.ChannelName));
        return Ok(summary);
    }

    [HttpPost]
    public IActionResult Create(ChannelRequest request)
    {
        ChannelDto newChannel = new ChannelDto
        {
            ChannelName = request.ChannelName,
        };

        Channels.Add(newChannel);

        return Created($"a/channels/{newChannel.ChannelId}", newChannel);
    }

}

