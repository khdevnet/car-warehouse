using CW.Core.Notifications;
using CW.WebApi.Abstractions.Services.Authentication;
using CW.WebApi.Hubs;
using CW.WebApi.Services.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW.WebApi
{
    public static class WebApiDiModule
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<INotificationSender, NotificationSender>();
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration) {

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.RequireHttpsMetadata = false;
               options.SaveToken = true;
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = configuration["Jwt:Issuer"],
                   ValidAudience = configuration["Jwt:Audience"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),
                   ClockSkew = TimeSpan.Zero
               };

           });

            services.AddAuthorization(config =>
            {
                config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
                config.AddPolicy(Policies.User, Policies.UserPolicy());
            });

            services.AddSingleton<JwtTokenProvider>();

            services.AddSingleton<IAuthenticationService, AuthenticationService>();

        }
    }
}
