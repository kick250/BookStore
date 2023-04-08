using Microsoft.AspNetCore.Mvc;

namespace Webapi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        throw new NotImplementedException();
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
