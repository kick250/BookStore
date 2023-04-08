using Entities;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Webapi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private AuthorsService AuthorsService { get; set; }

    public AuthorsController(AuthorsService authorsService)
    {
        AuthorsService = authorsService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return Ok(AuthorsService.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        try { 
            return Ok(AuthorsService.GetById(id));
        } catch (RecordNotFound ex)
        {
            return NotFound(new { Error = ex.Message });
        }
    }

    [HttpPost]
    public IActionResult Create([FromBody] Author author)
    {
        if (!ModelState.IsValid)
            return BadRequest(author);

        try
        {
            AuthorsService.Create(author);
            return Created("", author);
        } catch (EmailInUseException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromBody] Author author)
    {
        if (!ModelState.IsValid)
            return BadRequest(author);

        AuthorsService.Update(author);
        return Created("", author);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        throw new NotImplementedException();
    }
}
