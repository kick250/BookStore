namespace Infrastructure.Exceptions;

public class RecordNotFound : Exception
{
    public override string Message => "Esse dado não foi encontrado.";
}
