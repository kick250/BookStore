using Entities;
using Infrastructure.Exceptions;
using Repository;

namespace Services;

public class UsersService
{
    private BookStoreContext Context { get; set; }

    public UsersService(BookStoreContext context) 
    { 
        Context = context;
    }

    public void Create(User user)
    {
        if (EmailInUse(user.Username ?? ""))
            throw new EmailInUseException();

        user.EncodePassword();

        Context.Users.Add(user);
        Context.SaveChanges();
    }


    #region private

    private bool EmailInUse(string email)
    {
        return Context.Users.Any(author => author.Username == email);
    }

    #endregion
}