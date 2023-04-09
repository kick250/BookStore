using Infrastructure.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Webapi.Requests;

public class LoginRequest
{
    [Required(ErrorMessage = "O parametro 'username' é necessário."),
     EmailAddress(ErrorMessage = "O 'username' deve estar no formato correto.")]
    public string? Username { get; set; }
    [Required(ErrorMessage = "O parametro 'password' é necessário.")]
    public string? Password { get; set; }

    public string GetUsername()
    {
        if (Username == null)
            throw new RequiredParameterNotPresentException(nameof(Username));

        return Username;
    }

    public string GetPassword()
    {
        if (Password == null)
            throw new RequiredParameterNotPresentException(nameof(Password));

        return Password;
    }
}
