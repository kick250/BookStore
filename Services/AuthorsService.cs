using Entities;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace Services;

public class AuthorsService
{
    private BookStoreContext Context { get; set; }
    private IEnumerable<Author> Authors { get; set; }

    public AuthorsService(BookStoreContext context)
    {
        Context = context;
        Authors = Context.Authors
            .Include(author => author.Books);
    }

    public IEnumerable<Author> GetAll()
    {
        return Authors;
    }

    public Author GetById(int id)
    {
        Author? author = Authors.FirstOrDefault(author => author.Id == id);

        if (author == null)
            throw new RecordNotFound();

        return author;
    }
}