
using JobFinder.BLL.DTOs.Auth;
using JobFinder.BLL.Services.Interface;
using JobFinder.BLL.Services.Results;
using JobFinder.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using JobFinder.BLL.Options;
using Microsoft.Extensions.Options;

namespace JobFinder.BLL.Services.Implementation;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly JwtOptions _jwtOptions;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IJwtTokenService jwtTokenService,
        IOptions<JwtOptions> jwtOptions)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtTokenService = jwtTokenService;
        _jwtOptions = jwtOptions.Value;
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

    public async Task<LoginResponseDTO?> LoginAsync(
        LoginRequestDTO request)
    {
        var email = request.Email.Trim();

        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return null;
        }

        var signInResult = await _signInManager
            .CheckPasswordSignInAsync(
                user,
                request.Password,
                lockoutOnFailure: true);

        if (!signInResult.Succeeded)
        {
            return null;
        }

        var accessToken = _jwtTokenService.CreateAccessToken(new JwtCreateAccessTokenDTO
        {
            UserId = user.Id,
            Email = user.Email ?? email,
            FullName = user.FullName
        });

        var expiresAtUtc = DateTime.UtcNow.AddMinutes(
            _jwtOptions.AccessTokenExpirationMinutes);

        return new LoginResponseDTO
        {
            UserId = user.Id,
            FullName = user.FullName,
            Email = user.Email ?? email,
            AccessToken = accessToken,
            TokenType = "Bearer",
            ExpiresAtUtc = expiresAtUtc
        };
    }
}


