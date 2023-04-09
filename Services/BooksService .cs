using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace Services;

public class BooksService
{
    private BookStoreContext Context { get; set; }
    private IEnumerable<Book> Books { get; set; }

    public BooksService(BookStoreContext context)
    {
        Context = context;
        Books = Context.Books
            .Include(book => book.Authors);
    }

    public IEnumerable<Book> GetAll()
    {
        return Books;
    }

    public void Create(Book book)
    {
        Context.Books.Add(book);
        Context.SaveChanges();
    }
}