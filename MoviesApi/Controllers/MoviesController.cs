using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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
		_mapper        = mapper;
	}

	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public IActionResult
		GetMovies([FromQuery] uint skip = 0, [FromQuery] uint take = 50)
	{
		var dtos = _mapper.Map<List<ReadMovieDto>>(_moviesContext.Movies
			                                           .Skip((int)skip)
			                                           .Take((int)take)
			                                           .OrderBy(m => m.Title));

		return Ok(dtos.Skip((int)skip)
		              .Take((int)take)
		              .OrderBy(m => m.Title));
	}

	[HttpGet("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public IActionResult GetById(uint id)
	{
		var movie = _moviesContext.Movies.FirstOrDefault(m => m.Id == id);

		if (movie == null)
		{
			return NotFound();
		}

		var dto = _mapper.Map<ReadMovieDto>(movie);

		return Ok(dto);
	}

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
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

	[HttpPut("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public IActionResult UpdateMovie(uint id,
	                                 [FromBody] UpdateMovieDto newDto)
	{
		var movie = _moviesContext.Movies.FirstOrDefault(m => m.Id == id);

		if (movie == null)
		{
			return NotFound();
		}

		_mapper.Map(newDto, movie);
		_moviesContext.SaveChanges();

		return NoContent();
	}

	[HttpPatch("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public IActionResult PatchMovie(uint id,
	                                JsonPatchDocument<UpdateMovieDto>
		                                patchDocument)
	{
		var movie = _moviesContext.Movies.FirstOrDefault(m => m.Id == id);

		if (movie == null)
		{
			return NotFound();
		}

		var toBeUpated = _mapper.Map<UpdateMovieDto>(movie);

		patchDocument.ApplyTo(toBeUpated, ModelState);

		if (!TryValidateModel(toBeUpated))
		{
			return ValidationProblem(ModelState);
		}

		_moviesContext.SaveChanges();

		return NoContent();
	}

	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public IActionResult DeleteMovie(uint id)
	{
		var movie = _moviesContext.Movies.FirstOrDefault(m => m.Id == id);

		if (movie == null)
		{
			return NotFound();
		}

		_moviesContext.Movies.Remove(movie);
		_moviesContext.SaveChanges();

		return NoContent();
	}
}
