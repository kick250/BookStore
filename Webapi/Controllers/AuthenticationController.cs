using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Services;
using Webapi.Requests;

namespace Webapi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    public AuthenticationService AuthenticationService { get; set; }

    public AuthenticationController(AuthenticationService authenticationService)
    {
        AuthenticationService = authenticationService;
    }

    [HttpPost]
    public IActionResult Create(LoginRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(request);

        try
        {
            string token = AuthenticationService.Login(
                request.GetUsername(), 
                request.GetPassword()
            );

            return Ok(new { Token = token });
        } catch (LoginUnauthorizedException ex)
        {
            return Unauthorized(new { Error = ex.Message });
        } catch (RequiredParameterNotPresentException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}
