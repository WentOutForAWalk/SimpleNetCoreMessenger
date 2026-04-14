namespace Backend.Application.Interfaces.Services;

public interface IAuthService
{
    Task<bool> RegisterAsync(string name, string password);
    Task<bool> LoginAsync(string name, string password);
}

