using Microsoft.AspNetCore.Mvc;
using Entities;
using Webapp.APIs;

namespace Webapp.Controllers;

public class BooksController : Controller
{
    private BooksAPI BooksAPI { get; set; }
    private AuthorsAPI AuthorsAPI { get; set; }

    public BooksController(BooksAPI booksAPI, AuthorsAPI authorsAPI)
    {
        BooksAPI = booksAPI;
        AuthorsAPI = authorsAPI;
    }

    public ActionResult Index()
    {
        List<Book> books = BooksAPI.GetAll();
        return View(books);
    }

    public ActionResult Details(int id)
    {
        Book book = BooksAPI.GetById(id);
        return View(book);
    }

    public ActionResult New()
    {
        ViewBag.Authors = AuthorsAPI.GetAll();
        return View();
    }

    [HttpPost]
    public ActionResult Create([FromForm] Book book, List<int> authorIds)
    {
        if (!ModelState.IsValid) return View(nameof(New), book);

        try
        {
            BooksAPI.Create(book, authorIds);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View(nameof(New), book);
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
