using FluentValidation;
using JobFinder.BLL.DTOs.Auth;
using JobFinder.BLL.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.PL.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IValidator<RegisterRequestDTO> _registerValidator;

    public AuthController(
        IAuthService authService,
        IValidator<RegisterRequestDTO> registerValidator)
    {
        _authService = authService;
        _registerValidator = registerValidator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequestDTO request)
    {
        var validation = await _registerValidator.ValidateAsync(request);

        if (!validation.IsValid)
        {
            var validationErrors = validation.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(e => e.ErrorMessage).ToArray());

            // Create ValidationProblemDetails and populate its Errors
            var problemDetails = new ValidationProblemDetails();
            foreach (var kv in validationErrors)
            {
                problemDetails.Errors.Add(kv.Key, kv.Value);
            }

            return ValidationProblem(problemDetails);
        }

        var result = await _authService.RegisterAsync(request);

        if (!result.Succeeded)
        {
            var errors = result.Errors
                .Select(e => new { e.Code, e.Description })
                .ToArray();

            if (result.Errors.Any(e =>
                e.Code == "DuplicateEmail" ||
                e.Code == "DuplicateUserName"))
            {
                return Conflict(new { Errors = errors });
            }

            return BadRequest(new { Errors = errors });
        }

        var response = new RegisterResponseDTO
        {
            UserId = result.UserId!,
            FullName = request.FullName.Trim(),
            Email = request.Email.Trim(),
            Message = "Registration completed successfully."
        };

        return StatusCode(StatusCodes.Status201Created, response);
    }
}
