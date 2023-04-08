namespace Infrastructure.Exceptions;

public class EmailInUseException : Exception
{
    public override string Message => "Esse email já está em uso.";
}
