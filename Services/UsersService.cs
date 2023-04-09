using Entities;
using Infrastructure.Exceptions;
using Newtonsoft.Json.Linq;
using Repository;
using System.Runtime.CompilerServices;
using System.Text;

namespace Services;

public class UsersService
{
    private BookStoreContext Context { get; set; }
    private IEnumerable<User> Users { get; set; }

    public UsersService(BookStoreContext context) 
    { 
        Context = context;
        Users = context.Users;
    }

    public void Create(User user)
    {
        if (EmailInUse(user.Username ?? ""))
            throw new EmailInUseException();

        user.Password = EncodePassword(user.Password ?? "");

        Context.Users.Add(user);
        Context.SaveChanges();
    }

    public User Login(string username, string password)
    {
        string encodedPassword = EncodePassword(password);

        User? user = Users.FirstOrDefault(user => user.Username == username && user.Password == encodedPassword);

        if (user == null)
            throw new LoginUnauthorizedException();

        return user;
    }


    #region private

    private bool EmailInUse(string email)
    {
        return Context.Users.Any(author => author.Username == email);
    }

    public string EncodePassword(string value)
    {
        return Convert.ToBase64String(Encoding.Default.GetBytes(value));
    }

    #endregion
}