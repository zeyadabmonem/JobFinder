using JobFinder.BLL.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobFinder.BLL.Services.Interface
{
    public interface IJwtTokenService
    {
        string CreateAccessToken(JwtCreateAccessTokenDTO accessToken);
    }
}
