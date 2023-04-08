﻿using Entities;
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

    public void Create(Author author)
    {
        if (EmailInUse(author.Email ?? ""))
            throw new EmailInUseException();

        Context.Authors.Add(author);
        Context.SaveChanges();
    }

    public void Update(Author author)
    {
        Context.Authors.Update(author);
        Context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        Author author = GetById(id);

        if (author.HasSomeBook())
            throw new AuthorWithBooksException();

        Context.Authors.Remove(author);
        Context.SaveChanges();
    }

    #region private 

    private bool EmailInUse(string email)
    {
        return Context.Authors.Any(author => author.Email == email);
    }

    #endregion
}