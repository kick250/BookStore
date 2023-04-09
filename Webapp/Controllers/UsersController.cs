using Entities;
using Microsoft.AspNetCore.Mvc;
using Webapp.APIs;
using Webapp.Repositories;
using Webapp.Requests;

namespace Webapp.Controllers;

public class UsersController : Controller
{
    private UsersAPI UsersAPI { get; set; }
    private IAccountManager AccountManager { get; set; }

    public UsersController(UsersAPI usersAPI, IAccountManager accountManager)
    {
        UsersAPI = usersAPI;
        AccountManager = accountManager;
    }

    //public ActionResult Details(int id)
    //{
    //    return View();
    //}

    public ActionResult New()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(User user)
    {
        if (!ModelState.IsValid) return View(nameof(New), user);

        try
        {
            UsersAPI.Create(user);
            return Redirect("/");
        }
        catch (Exception ex) 
        {
            ViewBag.Error = ex.Message;
            return View(nameof(New), user);
        }
    }

    [HttpGet]
    public IActionResult Login([FromQuery] string? returnUrl) 
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginRequest request)
    {
        if (!ModelState.IsValid) return View(nameof(Login), request);

        try
        {
           var result = AccountManager.Login(request.GetUsername(), request.GetPassword()).Result;

            if (!result.Succeeded)
            {
                ViewBag.Error = "Usuário ou senha não correspondem.";
                ViewBag.ReturnUrl = request.ReturnUrl;
                return View(nameof(Login), request);
            }

            return Redirect(request.ReturnUrl ?? "/");
        } catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View(nameof(Login), request);
        }

    }

    public ActionResult Logout([FromQuery] string? returnUrl)
    {
        AccountManager.Logout();

        if (returnUrl == null) 
            returnUrl = "/";

        return RedirectToAction(nameof(Login), new { ReturnUrl = returnUrl });
    }
}
