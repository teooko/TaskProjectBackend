using System.Security.Claims;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskProjectBackend.Application.DTO;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    
    public AccountController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    [HttpPost, Authorize]
    
    [HttpPost, Authorize]
    public async Task<ActionResult<UserExtraDataDTO>> Post([FromBody] UserExtraDataDTO userExtraDataDto)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }
        
        await _userManager.AddClaimAsync(user, new Claim("Username", userExtraDataDto.Username));
        await _userManager.AddClaimAsync(user, new Claim("ProfileImageUrl", userExtraDataDto.ProfilePicturePath));

        // Return successful response
        return Ok(userExtraDataDto);
    }
}