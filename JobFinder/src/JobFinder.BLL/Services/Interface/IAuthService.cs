using JobFinder.BLL.DTOs.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using JobFinder.BLL.Services.Results;

namespace JobFinder.BLL.Services.Interface;

public interface IAuthService
{
    Task<RegisterServiceResult> RegisterAsync(RegisterRequestDTO request);

    Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO request);
}

