using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Helpers;

public static class JWTHelper
{
    public static string GetSignature()
    {
        DotNetEnv.Env.Load();
        string? salt = Environment.GetEnvironmentVariable("JWTSIGNATURE") ?? throw new ArgumentException("No enviroment variable founed ('JWTSIGNATURE')");
        return salt;
    }

    public static void ConfigureJWT(IServiceCollection services)
    {
        services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(
        options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                                System.Text.Encoding.UTF8.GetBytes(JWTHelper.GetSignature())
                            )

            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    if (context.Request.Headers.ContainsKey("x-token"))
                    {
                        context.Token = context.Request.Headers["x-token"];
                    }
                    return Task.CompletedTask;
                }
            };
        }

    );
    }
}