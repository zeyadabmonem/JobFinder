
using JobFinder.BLL.DTOs.Auth;
using JobFinder.BLL.Services.Interface;
using JobFinder.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace JobFinder.BLL.Services.Implementation;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthService(
        UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<RegisterServiceResult> RegisterAsync(
        RegisterRequestDTO request)
    {
        var email = request.Email.Trim();

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FullName = request.FullName.Trim(),
            CompanyName = string.IsNullOrWhiteSpace(request.CompanyName)
                ? null
                : request.CompanyName.Trim()
        };

        var identityResult = await _userManager.CreateAsync(
            user,
            request.Password);

        return new RegisterServiceResult
        {
            Succeeded = identityResult.Succeeded,
            UserId = identityResult.Succeeded ? user.Id : null,
            Errors = identityResult.Errors.ToArray()
        };
    }
}

