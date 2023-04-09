using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services;

public class AuthenticationService
{
    public const string ISSUER = "book-store-token";
    private string TokenSecret { get; set; }
    private UsersService UsersService { get; set; }

    public AuthenticationService(UsersService usersService, IConfiguration configuration)
    {
        UsersService = usersService;
        TokenSecret = configuration["TokenSecret"] ?? "";
    }

    public string Login(string username, string password)
    {
        User user = UsersService.Login(username, password);

        return GetToken(user);
    }

    #region private
    private string GetToken(User user)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim("sub", $"{user.Id}"),
            new Claim("email", $"{user.Username}"),
            new Claim("name", $"{user.Name}")
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.Default.GetBytes(TokenSecret);
        var securityToken = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = ISSUER,
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(securityToken);

        return tokenHandler.WriteToken(token);
    }
    #endregion
}
