using Microsoft.AspNetCore.Mvc;
using SimpleNetCore.Services;

namespace SimpleNetCore.Controllers;


[ApiController]
[Route("a/channels")]
public class MessageController : ControllerBase
{
    ChannelService messageService;

    public MessageController(ChannelService messageService)
    {
        this.messageService = messageService;
    }

}