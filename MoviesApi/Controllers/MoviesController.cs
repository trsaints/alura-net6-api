using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Data.Context;
using MoviesApi.Data.Dtos;
using MoviesApi.Data.Models;


namespace MoviesApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
	private readonly MoviesContext _moviesContext;
	private readonly IMapper       _mapper;
	
	public MoviesController(MoviesContext moviesContext, IMapper mapper)
	{
		_moviesContext = moviesContext;
		_mapper   = mapper;
	}

	[HttpGet]
	public IActionResult GetMovies([FromQuery] uint skip = 0, [FromQuery] uint take = 50)
	{
		return Ok(_moviesContext.Movies.Skip((int)skip).Take((int)take).OrderBy(m => m.Title));
	}

	[HttpGet("{id}")]
	public IActionResult GetById(uint id)
	{
		var movie = _moviesContext.Movies.FirstOrDefault(m => m.Id == id);

		if (movie == null)
		{
			return NotFound();
		}

		return Ok(movie);
	}

	[HttpPost]
	public IActionResult AddMovie([FromBody] CreateMovieDto dto)
	{
		var movie = _mapper.Map<Movie>(dto);
		
		if (ModelState.IsValid == false)
		{
			return BadRequest(ModelState);
		}

		_moviesContext.Movies.Add(movie);
		_moviesContext.SaveChanges();

		return Created($"api/movies/{movie.Id}", dto);
	}
}
