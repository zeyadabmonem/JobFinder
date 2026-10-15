using FluentValidation;
using FluentValidation.AspNetCore;
using JobFinder.API.Extensions;
using JobFinder.BLL.Services.Implementation;
using JobFinder.BLL.Services.Interface;
using JobFinder.BLL.Validators;
using JobFinder.DAL.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                )
            );

            builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddValidatorsFromAssemblyContaining<RegisterDTOValidator>();

            builder.Services.AddOpenApi();

            // Identity Configuration
            builder.Services.AddIdentityServices();

            // JWT Configuration
            builder.Services.AddJwtAuthentication(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}