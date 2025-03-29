using Microsoft.AspNetCore.Mvc;
using MoviesApi.Data.Models;


namespace MoviesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
	private static IEnumerable<Movie> _movies = new List<Movie>();
	
	[HttpGet]
	public IActionResult GetMovies()
	{
		return Ok(_movies);
	}

	[HttpGet("{id}")]
	public IActionResult GetById(uint id)
	{
		var movie = _movies.FirstOrDefault(m => m.Id == id);
		
		if (movie == null)
		{
			return NotFound();
		}
		
		return Ok(movie);
	}
	
	[HttpPost]
	public IActionResult AddMovie([FromBody] Movie m)
	{
		if (ModelState.IsValid == false)
		{
			return BadRequest(ModelState);
		}
		
		var enumerable = _movies.Append(m);
		
		_movies = enumerable;
		
		return Created($"api/movies/{m.Id}", m);
	}
}
