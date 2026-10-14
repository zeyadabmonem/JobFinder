using JobFinder.API.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JobFinder.API.Extensions;

public static class JwtAuthenticationExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,IConfiguration configuration)
    {
        var section = configuration.GetSection(JwtOptions.SectionName);

        services.AddOptions<JwtOptions>()
            .Bind(section)
            .Validate(
                options =>
                    !string.IsNullOrWhiteSpace(options.Issuer) &&
                    !string.IsNullOrWhiteSpace(options.Audience) &&
                    !string.IsNullOrWhiteSpace(options.Key) &&
                    options.Key.Length >= 32 &&
                    options.AccessTokenExpirationMinutes > 0 &&
                    options.RefreshTokenExpirationDays > 0,
                "JWT configuration is missing or invalid.")
            .ValidateOnStart();

        var jwt = section.Get<JwtOptions>()
            ?? throw new InvalidOperationException(
                "JWT configuration section is missing.");

        if (string.IsNullOrWhiteSpace(jwt.Issuer) ||
            string.IsNullOrWhiteSpace(jwt.Audience) ||
            string.IsNullOrWhiteSpace(jwt.Key) ||
            jwt.Key.Length < 32)
        {
            throw new InvalidOperationException(
                "JWT Issuer, Audience, and a strong Key are required.");
        }

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;

                options.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme;

                options.DefaultScheme = 
                    JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwt.Issuer,

                        ValidateAudience = true,
                        ValidAudience = jwt.Audience,

                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwt.Key))
                    };
            });

        services.AddAuthorization();

        return services;
    }
}
