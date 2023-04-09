using Infrastructure.Exceptions;
using Newtonsoft.Json;
using Webapp.Requests;

namespace Webapp.APIs;

public class AuthenticationAPI : IAPI
{
    public AuthenticationAPI(IConfiguration configuration)
        : base(configuration["WebapiHost"]) { }

    public string Authenticate(string username, string password)
    {
        var loginParams = new
        {
            Username = username,
            Password = password
        };

        var response = Post($"/Authentication", loginParams).Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);

        string jsonResult = response.Content.ReadAsStringAsync().Result;

        TokenRequest? result = JsonConvert.DeserializeObject<TokenRequest>(jsonResult);

        if (result == null || result.Token == null)
            throw new Exception("Ocorreu um erro desconhecido.");

        return result.Token;
    }
}
