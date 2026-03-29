using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SimpleNetCore.Controllers;

[ApiController]
[Route("/")]
public class HomeController : ControllerBase
{
    private readonly IWebHostEnvironment _env;
    public HomeController(IWebHostEnvironment env)
    {
        _env = env;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var filePath = Path.Combine(_env.WebRootPath, "index.html");

        return PhysicalFile(filePath, "text/html");
    }


}

