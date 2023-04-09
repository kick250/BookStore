using Entities;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Webapi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private UsersService UsersService { get; set; }

    public UsersController(UsersService usersService)
    {
        UsersService = usersService;
    }

    //[HttpGet("{id}")]
    //public IActionResult Show(int id)
    //{
    //    throw new NotImplementedException();
    //}

    [HttpPost]
    public IActionResult Post([FromBody] User user)
    {
        if (!ModelState.IsValid) return BadRequest(user);

        try
        {
            UsersService.Create(user);
            return Created("", user);
        } catch (EmailInUseException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}
