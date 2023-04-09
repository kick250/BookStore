using Webapp.Models;

namespace Webapp.Helpers;

public class SessionHelper
{
    HttpContext? HttpContext { get; set; }

    public SessionHelper(HttpContext context)
    {
        HttpContext = context;
    }

    public bool TokenIsPresent()
    {
        return !string.IsNullOrEmpty(GetToken());
    }

    public string GetToken()
    {
        if (HttpContext == null)
            return "";

        return HttpContext.Session.GetString(Account.SESSION_TOKEN_KEY) ?? "";
    }
}
