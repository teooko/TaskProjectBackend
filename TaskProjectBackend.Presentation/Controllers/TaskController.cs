using Microsoft.AspNetCore.Mvc;

namespace TaskProjectBackend.Presentation.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TaskController : Controller
{

    /*
    private readonly GenreService _genreService = new GenreService();
    
    [HttpPost]
    public ActionResult<Task> Post([FromBody] DGenre genre)
    {
        DGenre postedGenre = _genreService.Post(genre);
        
        if(postedGenre == null)
            return this.BadRequest("Name required");
            
        return this.Ok(postedGenre);
    }
    
    [HttpGet]
    public ActionResult<DGenre> Get()
    {
        return this.Ok(_genreService.Get());
    }
    
    */
}