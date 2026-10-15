using System;
using System.Collections.Generic;
using System.Text;

namespace JobFinder.BLL.DTOs.Auth
{
    public class RegisterRequestDTO
    {
        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string ConfirmPassword { get; set; } = string.Empty;

        public string? CompanyName { get; set; }
    }
}
