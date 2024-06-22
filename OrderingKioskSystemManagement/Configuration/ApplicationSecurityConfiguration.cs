using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OrderingKioskSystem.Application.Common.Interfaces;
using OrderingKioskSystemManagement.Api.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace OrderingKioskSystemManagement.Api.Configuration
{
    public static class ApplicationSecurityConfiguration
    {
        public static IServiceCollection ConfigureApplicationSecurity(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IJwtService, JwtService>();
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            services.AddHttpContextAccessor();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme; // Add this line
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("from sonhohuu deptrai6mui with love")),
                };
            })
            .AddGoogle(options =>
            {
                options.ClientId = configuration.GetSection("Authentication:Google:ClientId").Get<string>();
                options.ClientSecret = configuration.GetSection("Authentication:Google:ClientSecret").Get<string>();
                options.CallbackPath = "/dang-nhap-tu-google";
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme); // Ensure cookies are used

            services.AddAuthorization(ConfigureAuthorization);

            return services;
        }

        private static void ConfigureAuthorization(AuthorizationOptions options)
        {
            // Configure policies and other authorization options here. For example:
            // options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("role", "employee"));
            options.AddPolicy("Business", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("role", "Business");
            });
            options.AddPolicy("Manager", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("role", "Manager");
            });
            options.AddPolicy("Kiosk", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("role", "Kiosk");
            });
        }
    }
}
