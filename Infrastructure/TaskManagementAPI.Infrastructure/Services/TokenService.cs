using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.DTOs.Authentication;
using TaskManagementAPI.Domain.Entities.Identity;

namespace TaskManagementAPI.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly TokenSettings _tokenSettings;
    private readonly IUserService _userService;

    public TokenService(IOptions<TokenSettings> options, IUserService userService)
    {
        _tokenSettings = options.Value;
        _userService = userService;
    }
    public async Task<TokenDTO> CreateAccessTokenAsync(AppUser appUser, IList<string> roles)
    {
        var claims = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, appUser.Email),
            new(JwtRegisteredClaimNames.Name, appUser.UserName)
        };

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_tokenSettings.SecurityKey));

        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwtSecurityToken = new (
            issuer: _tokenSettings.Issuer,
            audience: _tokenSettings.Audience,
            expires: DateTime.UtcNow.AddSeconds(_tokenSettings.AccessTokenExpirationInMinutes * 60),
            claims: claims,
            signingCredentials: signingCredentials
            );

        await _userService.AddClaimsAsync(appUser, claims);

        TokenDTO tokenDTO = new();
        tokenDTO.AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        tokenDTO.RefreshToken = CreateRefreshToken();
        tokenDTO.Expiration = DateTime.UtcNow.AddSeconds(_tokenSettings.AccessTokenExpirationInMinutes * 60);

        return tokenDTO;
    }
    private string CreateRefreshToken()
    {
        byte[] numbers = new byte[32];
        using RandomNumberGenerator random = RandomNumberGenerator.Create();
        random.GetBytes(numbers);
        return Convert.ToBase64String(numbers);
    }
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        TokenValidationParameters tokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = _tokenSettings.Audience,
            ValidIssuer = _tokenSettings.Issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.SecurityKey)),
            ValidateLifetime = true,
        };
        JwtSecurityTokenHandler tokenHandler = new();
        ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken
            || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Token not found");
        }
        return claimsPrincipal;
    }
}
