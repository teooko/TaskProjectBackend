using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] ApplicationUser model)
    {
        var user = new ApplicationUser
        {
            UserName = model.UserName,
            Email = model.Email,
            // Set other properties
        };

        var result = await _userManager.CreateAsync(user);

        if (result.Succeeded)
        {
            // Your registration success logic
            return Ok(new { Message = "Registration successful" });
        }

        // If registration fails, return errors
        return BadRequest(new { Errors = result.Errors });
    }
}