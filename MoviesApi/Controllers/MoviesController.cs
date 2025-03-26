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
	
	[HttpPost]
	public void AddMovie([FromBody] Movie m)
	{
		var enumerable = _movies.Append(m);
		
		_movies = enumerable;
	}
}
