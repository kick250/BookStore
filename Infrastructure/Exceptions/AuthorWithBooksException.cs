namespace Infrastructure.Exceptions;

public class AuthorWithBooksException : Exception
{
    public override string Message => "Impossível apagar um author que possui livros cadastrados.";
}
