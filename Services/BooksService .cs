using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;
using Infrastructure.Exceptions;

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

    public Book GetById(int id)
    {
        Book? book = Books.FirstOrDefault(book => book.Id == id);

        if (book == null)
            throw new RecordNotFound();

        return book;
    }

    public Book GetById(int? id)
    {
        if (id == null)
            throw new RecordNotFound();

        Book? book = Books.FirstOrDefault(book => book.Id == id);

        if (book == null)
            throw new RecordNotFound();

        return book;
    }

    public void Create(Book book)
    {
        Context.Books.Add(book);
        Context.SaveChanges();
    }

    public void Update(Book book)
    {
        Book bookToUpdate = GetById(book.Id);

        bookToUpdate.UpdateFrom(book);

        Context.Books.Update(bookToUpdate);
        Context.SaveChanges();
    }
}