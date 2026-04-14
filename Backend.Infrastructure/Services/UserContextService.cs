using Backend.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Backend.Infrastructure.Services;

public class UserContextService : IUserContextService
{
    private IHttpContextAccessor _httpContextAccessor;
    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public string GetUserName()
    {
        return _httpContextAccessor.HttpContext?.User?.Identity?.Name!;
    }
    public string GetUserId()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
    }
}

