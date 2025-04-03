using System.ComponentModel.DataAnnotations;


namespace MoviesApi.Data.Dtos;


public class CreateMovieDto
{
	[Required]
	[StringLength(128)]
	public string? Title { get; set; }
	
	[Required]
	[StringLength(128)]
	public string? Genre { get; set; }
	
	[Required]
	[Range(70, 600)]
	public uint Duration { get; set; }
}
