using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Webapp.Helpers;

namespace Webapp.Controllers;

public class AuthorizedController : Controller
{
    protected string? SessionToken { get; set; }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        SessionHelper sessionHelper = new SessionHelper(context.HttpContext);

        if (!sessionHelper.TokenIsPresent()) context.Result = Logout();

        SessionToken = sessionHelper.GetToken();

        SetAPIToken();

        base.OnActionExecuting(context);
    }
    protected virtual void SetAPIToken() { }

    #region private

    private RedirectToRouteResult Logout()
    {
        return new RedirectToRouteResult(new RouteValueDictionary(new
        {
            controller = "Users",
            action = "Logout"
        }));
    }

    #endregion
}
