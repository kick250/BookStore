namespace Infrastructure.Exceptions;

public class AuthorWithBooksException : Exception
{
    public override string Message => "Impossível apagar um autor que possui livros cadastrados.";
}
