using Backend.Application.DTO.Auth;
using Backend.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;


namespace Backend.API.Controllers;


[ApiController]
[Route("a/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] LoginRequest request)
    {
        var succes = await _authService.RegisterAsync(request.Name, request.Password);
        return succes ? Ok() : BadRequest();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request) 
    {
        var succes = await _authService.LoginAsync(request.Name, request.Password);
        return succes ? Ok("successful login") : BadRequest("Incorrect login or password");
    }

}

