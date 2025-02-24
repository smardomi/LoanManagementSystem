using LoanManagementSystem.Application.Common.Interfaces;
using LoanManagementSystem.Domain.Constants;
using LoanManagementSystem.Domain.Entities.Identity;
using LoanManagementSystem.Infrascructure.Data;
using LoanManagementSystem.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection;
public static class ServiceRegistrar
{
    public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("LoanManagementSystemDb");


        builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
        });

        builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
        {          
            options.User.RequireUniqueEmail = true; 
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@"; // کاراکترهای مجاز برای نام کاربری

          
            options.Password.RequireDigit = true; 
            options.Password.RequireLowercase = true; 
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6; 
            options.Password.RequiredUniqueChars = 1; 

         
            options.Lockout.AllowedForNewUsers = true; 
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30); 

        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                               .AddJwtBearer(options =>
                               {
                                   options.TokenValidationParameters = new TokenValidationParameters
                                   {
                                       ValidateIssuer = true,
                                       ValidateAudience = true,
                                       ValidateLifetime = true,
                                       ValidIssuer = builder.Configuration["Jwt:Issuer"],
                                       ValidAudience = builder.Configuration["Jwt:Audience"],
                                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                                   };
                               });


        builder.Services.AddSingleton(TimeProvider.System);
        builder.Services.AddScoped<IIdentityService, IdentityService>();

        builder.Services.AddAuthorization(options =>
            options.AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator)));
    }
}
