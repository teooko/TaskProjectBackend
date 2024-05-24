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
    public async Task<ActionResult<string>> Post([FromBody] UserExtraDataDTO userExtraDataDto)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }
        await _userManager.AddClaimAsync(user, new Claim("Username", userExtraDataDto.Username));
        
        if(!string.IsNullOrEmpty(userExtraDataDto.ProfilePicturePath))
        {
            await _userManager.AddClaimAsync(user, new Claim("ProfilePicturePath", userExtraDataDto.ProfilePicturePath));
            await _userManager.AddClaimAsync(user, new Claim("ProfilePictureBase64", userExtraDataDto.ProfilePictureBase64));
        }
        var result = await _userManager.GetClaimsAsync(user);
        
        return Ok(result);
    }
    
    [HttpGet, Authorize]
    public async Task<ActionResult<string>> Get()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        var result = await _userManager.GetClaimsAsync(user);
        
        return Ok(result);
    }
    
    [HttpGet("{userId}"), Authorize]
    public async Task<ActionResult<string>> Get(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        var result = await _userManager.GetClaimsAsync(user);
        
        return Ok(result);
    }
    
}