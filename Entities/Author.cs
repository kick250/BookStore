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
}
