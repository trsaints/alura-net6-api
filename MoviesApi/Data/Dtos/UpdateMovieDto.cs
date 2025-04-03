using System.ComponentModel.DataAnnotations;


namespace MoviesApi.Data.Dtos;


public class UpdateMovieDto
{
	[StringLength(128)]
	public string? Title { get; set; }
	
	[StringLength(128)]
	public string? Genre { get; set; }
	
	[Range(70, 600)]
	public uint Duration { get; set; }
}
