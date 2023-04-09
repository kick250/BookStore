namespace Infrastructure.Exceptions;

public class RequiredParameterNotPresentException : Exception
{
    private string RequiredParameter { get; set; }

    public RequiredParameterNotPresentException(string requiredParameter)
        : base()
    {
        RequiredParameter = requiredParameter;
    }

    public override string Message => $"O parametro {RequiredParameter} não foi enviado.";
}
