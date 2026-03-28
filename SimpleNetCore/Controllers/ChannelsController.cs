using Microsoft.AspNetCore.Mvc;
using SimpleNetCore.DTO.Channel;
using SimpleNetCore.Services;
using System.Reflection.Metadata.Ecma335;

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
    public IActionResult GetAll()
    {   // channel summary
        return Ok(channelService.GetChannelSummary());
    }

    [HttpPost]
    public IActionResult Create(ChannelRequest request)
    {   // adds a channel to data
        var newChannel = channelService.AddChannel(request);

        return Created($"a/channels/{newChannel.ChannelId}", newChannel);
    }

    [HttpPut("{ChannelId:guid}")]
    public IActionResult Update(Guid ChannelId, ChannelRequest request)
    {
        if (channelService.EditChannel(ChannelId, request)){
            return Ok();
        }
        return NotFound();
    }


    







}
