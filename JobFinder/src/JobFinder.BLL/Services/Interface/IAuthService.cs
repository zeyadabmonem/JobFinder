using JobFinder.BLL.DTOs.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

using JobFinder.BLL.DTOs.Auth;

namespace JobFinder.BLL.Services.Interface;

public interface IAuthService
{
    Task<RegisterServiceResult> RegisterAsync(RegisterRequestDTO request);
}

