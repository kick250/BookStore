using Microsoft.AspNetCore.Mvc;
using Entities;
using Services;
using Webapi.Requests;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace Webapi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
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
        try
        {
            Book book = BooksService.GetById(id);
            return Ok(book);
        } catch(RecordNotFound ex)
        {
            return NotFound(new { ex.Message });
        }
    }

    [HttpPost]
    public IActionResult Create([FromBody] BookRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(request);

        Book book = request.GetBook();

        List<Author> authors = AuthorsService.GetByIds(request.GetAuthorIds());
        book.Authors = authors;

        BooksService.Create(book);

        return Created("", book);
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromBody] BookRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(request);

        Book book = request.GetBook();

        List<Author> authors = AuthorsService.GetByIds(request.GetAuthorIds());
        book.Authors = authors;

        BooksService.Update(book);

        return Ok(book);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        BooksService.DeleteById(id);

        return NoContent();
    }
}
