using Microsoft.AspNetCore.Identity;
using Webapp.Models;

namespace Webapp.Repositories;

public class AccountRepository : IUserStore<Account>
{
    public void Dispose()
    {
    }

    public Task<string?> GetNormalizedUserNameAsync(Account user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.Username);
    }

    public Task<string> GetUserIdAsync(Account user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.GetId());
    }

    public Task<string?> GetUserNameAsync(Account user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.Username);
    }

    #region Not in use
    public Task<IdentityResult> CreateAsync(Account user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> DeleteAsync(Account user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Account?> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Account?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetNormalizedUserNameAsync(Account user, string? normalizedName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetUserNameAsync(Account user, string? userName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> UpdateAsync(Account user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    #endregion
}
