using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace JobFinder.BLL.Services.Results;

public class RegisterServiceResult
{
    public bool Succeeded { get; init; }

    public string? UserId { get; init; }

    public IReadOnlyCollection<IdentityError> Errors { get; init; }
        = Array.Empty<IdentityError>();
}

