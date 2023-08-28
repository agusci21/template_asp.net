using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Helpers;
using Microsoft.IdentityModel.Tokens;

namespace Feature.User;

public class TokenRepository : ITokenRepository
{
    public Task<string> GetTokenAsync(string UserId)
    {
        var claims = new[] { new Claim("user_id", UserId), };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTHelper.GetSignature()));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: credentials
        );

        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
    }

    public Task<string?> GetUserIdFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);

        var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "user_id");


        return Task.FromResult(userIdClaim?.Value);

    }

}
