using Auth.AuthServices;
using Auth.IAuthServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth
{
    public static class AuthDependency
    {
        public static IServiceCollection AuthDependencyInjection(this IServiceCollection service,IConfiguration configuration)
        {
            service.AddScoped<IAuthTokenGenerator, AuthTokenGenerator>();
            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwt => {
                    jwt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                    };
                    
                });
            /*service.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly",
                    policy => policy
                    .RequireAssertion(
                        x => AssertClaim(x, UserRoles.Admin)
                        ));
                options.AddPolicy("Registered",
                    policy => policy
                    .RequireAssertion(
                        x => AssertClaim(x, UserRoles.RegisteredUser)
                        ));
               
            });*/
            return service;
        }
    }
}
