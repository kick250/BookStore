using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Book
{
    public int? Id { get; set; }
    [Required(ErrorMessage = "Um livro precisa de um nome")]
    public string? Title { get; set; }
    [Required(ErrorMessage = "Um livro precisa de um ISBN")]
    public string? ISBN { get; set; }
    [Required(ErrorMessage = "Um livro precisa de uma data de lançamento")]
    public DateTime? ReleaseDate { get; set; }
    public List<Author>? Authors { get; set; }

    public List<int?> GetAuthorsIds()
    {
        if (Authors == null) return new List<int?>();

        return Authors.Select(x => x.Id).ToList();
    }

    public void UpdateFrom(Book book)
    {
        Id = book.Id;
        Title = book.Title;
        ISBN = book.ISBN;
        ReleaseDate = book.ReleaseDate;
        Authors = book.Authors;
    }
}