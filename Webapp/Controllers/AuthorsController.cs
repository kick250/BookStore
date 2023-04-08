using Microsoft.AspNetCore.Mvc;
using Webapp.APIs;
using Entities;

namespace Webapp.Controllers;

public class AuthorsController : Controller
{
    private AuthorsAPI AuthorsAPI { get; set; }

    public AuthorsController(AuthorsAPI authorsAPI)
    {
        AuthorsAPI = authorsAPI;
    }

    public ActionResult Index()
    {
        List<Author> authors = AuthorsAPI.GetAll();

        return View(authors);
    }

    public ActionResult Details(int id)
    {
        Author author = AuthorsAPI.GetById(id);
        return View(author);
    }

    public ActionResult New()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Author author)
    {
        if (!ModelState.IsValid)
            return View("New", author);

        try
        {
            AuthorsAPI.Create(author);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View("New", author);
        }
    }

    public ActionResult Edit(int id)
    {
        return View();
    }

    [HttpPost]
    public ActionResult Edit(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    public ActionResult Delete(int id)
    {
        return View();
    }

    [HttpPost]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
