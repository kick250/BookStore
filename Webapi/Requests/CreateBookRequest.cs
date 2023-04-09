using Entities;
using Infrastructure.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Webapi.Requests;

public class CreateBookRequest
{
    public Book? Book { get; set; }
    [Required(ErrorMessage = "É necessário informar os autores do livro.")]
    public List<int?>? AuthorIds { get; set; }


    public Book GetBook()
    {
        if (Book == null)
            throw new RequiredParameterNotPresentException("Book");

        return Book;
    }
    public List<int?> GetAuthorIds()
    {
        if (AuthorIds == null)
            throw new RequiredParameterNotPresentException("AuthorIds");

        return AuthorIds;
    }
}
