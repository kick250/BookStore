using Microsoft.AspNetCore.Mvc;
using Entities;
using Services;
using Webapi.Requests;

namespace Webapi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private BooksService BooksService { get; set; }
    private AuthorsService AuthorsService { get; set; }

    public BooksController(BooksService booksService, AuthorsService authorsService)
    {
        BooksService = booksService;
        AuthorsService = authorsService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        IEnumerable<Book> books = BooksService.GetAll();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateBookRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(request);

        Book book = request.GetBook();

        List<Author> authors = AuthorsService.GetByIds(request.GetAuthorIds());
        book.Authors = authors;

        BooksService.Create(book);

        return Created("", book);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] string value)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        throw new NotImplementedException();
    }
}
