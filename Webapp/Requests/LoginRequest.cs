using Infrastructure.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Webapp.Requests;

public class LoginRequest
{
    [Required(ErrorMessage = "É necessário um email para logar"),
     EmailAddress(ErrorMessage = "O email deve estar no formato correto")]
    public string? Username { get; set; }
    [Required(ErrorMessage = "É necessário uma senha para logar")]
    public string? Password { get; set; }
    public string? ReturnUrl { get; set; } = "/";

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
