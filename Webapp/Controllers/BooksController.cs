using Microsoft.AspNetCore.Mvc;
using Entities;
using Webapp.APIs;

namespace Webapp.Controllers;

public class BooksController : Controller
{
    private BooksAPI BooksAPI { get; set; }

    public BooksController(BooksAPI booksAPI)
    {
        BooksAPI = booksAPI;
    }

    public ActionResult Index()
    {
        List<Book> books = BooksAPI.GetAll();
        return View(books);
    }

    public ActionResult Details(int id)
    {
        return View();
    }

    public ActionResult New()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(IFormCollection collection)
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
    public ActionResult Destroy(int id, IFormCollection collection)
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
