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
        SetAuthors();
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
        SetAuthors();
        Book book = BooksAPI.GetById(id);
        ViewBag.SelectedAuthorIds = book.GetAuthorsIds();
        return View(book);
    }

    [HttpPost]
    public ActionResult Update([FromForm] Book book, List<int> authorIds)
    {
        if (!ModelState.IsValid) return View(nameof(Edit), book);

        try
        {
            BooksAPI.Update(book, authorIds);

            return RedirectToAction(nameof(Details), new { id = book.Id });
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            SetAuthors();
            ViewBag.SelectedAuthorIds = authorIds.Select(x => (int?)x).ToList();
            return View(nameof(Edit), book);
        }
    }

    public ActionResult Delete(int id)
    {
        Book book = BooksAPI.GetById(id);
        return View(book);
    }

    [HttpPost]
    public ActionResult Destroy(int id)
    {
        try
        {
            BooksAPI.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return RedirectToAction(nameof(Delete), new { id = id });
        }
    }

    #region private
    
    private void SetAuthors()
    {
        ViewBag.Authors = AuthorsAPI.GetAll();
    }

    #endregion
}
