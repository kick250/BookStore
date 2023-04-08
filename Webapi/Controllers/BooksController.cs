using Microsoft.AspNetCore.Mvc;
using Entities;
using Services;

namespace Webapi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private BooksService BooksService { get; set; }

    public BooksController(BooksService booksService)
    {
        BooksService = booksService;
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
    public IActionResult Create([FromBody] string value)
    {
        throw new NotImplementedException();
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
