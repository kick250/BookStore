using Microsoft.AspNetCore.Mvc;
using Webapp.APIs;
using Entities;
using Microsoft.AspNetCore.Authorization;

namespace Webapp.Controllers;

[Authorize]
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
            return View(nameof(New), author);

        try
        {
            AuthorsAPI.Create(author);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View(nameof(New), author);
        }
    }

    public ActionResult Edit(int id)
    {
        Author author = AuthorsAPI.GetById(id);
        return View(author);
    }

    [HttpPost]
    public ActionResult Update(Author author)
    {
        if (!ModelState.IsValid)
            return View(nameof(Edit), author);

        try
        {
            AuthorsAPI.Update(author);

            return RedirectToAction(nameof(Details), new { Id = author.Id });
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View(nameof(Edit), author);
        }
    }

    public ActionResult Delete(int id)
    {
        Author author = AuthorsAPI.GetById(id);
        return View(author);
    }

    [HttpPost]
    public ActionResult Destroy(int id)
    {
        try
        {
            AuthorsAPI.DeleteById(id);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View(nameof(Delete), AuthorsAPI.GetById(id));
        }
    }
}
