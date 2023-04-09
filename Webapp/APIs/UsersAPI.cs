using Entities;
using Infrastructure.Exceptions;

namespace Webapp.APIs;

public class UsersAPI : IAPI
{
    public UsersAPI(IConfiguration configuration)
        : base(configuration["WebapiHost"]) { }

    public void Create(User user)
    {
        var response = Post("/Users", user).Result;

        if (!response.IsSuccessStatusCode)
            throw new APIErrorException(response);
    }
}
