using Entities;
using Microsoft.AspNetCore.Mvc;
using Webapp.APIs;

namespace Webapp.Controllers;

public class UsersController : Controller
{
    private UsersAPI UsersAPI;

    public UsersController(UsersAPI usersAPI)
    {
        UsersAPI = usersAPI;
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
}
