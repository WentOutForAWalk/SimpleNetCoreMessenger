using Backend.Application.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace Backend.Infrastructure.Services;
public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public async Task<bool> RegisterAsync(string name, string password) 
    {
        var user = new IdentityUser { UserName = name };
        var result = await _userManager.CreateAsync(user,password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            Console.Write(errors);
        }
        return result.Succeeded;
    }
    public async Task<bool> LoginAsync(string name, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(name, password, true, false);
        return result.Succeeded;
    }
}

