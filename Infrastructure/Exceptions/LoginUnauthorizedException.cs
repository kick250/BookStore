namespace Infrastructure.Exceptions;

public class LoginUnauthorizedException : Exception
{
    public override string Message => "Email ou senha não correspondem.";
}
