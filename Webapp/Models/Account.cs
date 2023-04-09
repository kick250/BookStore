using System.IdentityModel.Tokens.Jwt;

namespace Webapp.Models;

public class Account
{
    public const string SESSION_TOKEN_KEY = "account-session-token";

    public string? Username { get; set; }
    public string? Name { get; set; }
    public string? Token { get; set; }
    public string? Password { get; set; }

    public string GetId()
    {
        var id = GetKeyFromToken("sub");
        return id;
    }

    #region private
    private string GetKeyFromToken(string type)
    {
        return DecodeToken().Claims.First(x => x.Type == type).Value;
    }

    private JwtSecurityToken DecodeToken()
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonResult = handler.ReadJwtToken(Token);

        return jsonResult as JwtSecurityToken;
    }
    #endregion
}
