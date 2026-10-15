using System;
using System.Collections.Generic;
using System.Text;


namespace JobFinder.BLL.DTOs.Auth;

public class LoginResponseDTO
{
    public string UserId { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string AccessToken { get; set; } = string.Empty;

    public string TokenType { get; set; } = "Bearer";

    public DateTime ExpiresAtUtc { get; set; }
}
