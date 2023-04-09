using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities;

public class User
{
    public int? Id { get; set; }
    [Required(ErrorMessage = "É necessário um nome para o usuário")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "É necessário um email para o usuário"),
     EmailAddress(ErrorMessage = "O email deve estar no formato correto")]
    public string? Username { get; set; }
    [Required(ErrorMessage = "É necessário uma senha para o usuário")]
    public string? Password { get; set; }

    public void EncodePassword()
    {
        Password = Convert.ToBase64String(Encoding.Default.GetBytes(Password ?? ""));
    }
}
