using System.ComponentModel.DataAnnotations;

namespace Entities;
public class Author
{
    public int? Id { get; set; }
    [Required(ErrorMessage = "O nome do autor é necessário.")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "O sobrenome do autor é necessário.")]
    public string? LastName { get; set; }
    [Required(ErrorMessage = "O email do autor é necessário."),
     EmailAddress(ErrorMessage = "O email precisa estar no formato correto.")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "A data de aniversário do autor é necessárioa")]
    public DateTime? Birthdate { get; set; }
    public List<Book>? Books { get; set; }

    public string GetFullName()
    {
        return $"{Name} {LastName}";
    }

    public bool HasSomeBook()
    {
        if (Books == null) return false;

        return Books.Count > 0;
    }

    public string GetFormattedBirthdate()
    {
        if (Birthdate == null) return "";

        DateTime releaseDate = (DateTime)Birthdate;

        return releaseDate.ToString("dd/MM/yyyy");
    }
}
