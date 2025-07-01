using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.ServiceExtentions;

public static class IdentifyServiceExtention
{
    public static IServiceCollection AddIdentifyService(this IServiceCollection services,IConfiguration configure)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options =>
    {
        var tokenKey = configure["TokenKey"] ?? throw new Exception("Key is not Found");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };

    }
);
        return services;
    }
}
