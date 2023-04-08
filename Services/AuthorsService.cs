using Entities;
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
}