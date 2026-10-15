using JobFinder.BLL.DTOs.Auth;
using JobFinder.BLL.Options;
using JobFinder.BLL.Services.Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JobFinder.BLL.Services.Implementation
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtOptions _jwtOptions;

        public JwtTokenService(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public string CreateAccessToken(JwtCreateAccessTokenDTO accessToken)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub,accessToken.UserId),
                new(JwtRegisteredClaimNames.Email,accessToken.Email),
                new("name",accessToken.Email),
                new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())

            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtOptions.Key));

            var credentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);

            var expiresAt = DateTime.UtcNow.AddMinutes(
                _jwtOptions.AccessTokenExpirationMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expiresAt,
                signingCredentials: credentials

                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
