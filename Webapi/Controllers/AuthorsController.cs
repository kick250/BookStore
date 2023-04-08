using Infrastructure.Exceptions;
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
