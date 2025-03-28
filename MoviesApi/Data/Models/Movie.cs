using System.ComponentModel.DataAnnotations;


namespace MoviesApi.Data.Models;


public class Movie
{
	[Required]
	[MaxLength(128)]
	public string? Title { get; set; }
	
	[Required]
	[MaxLength(128)]
	public string? Genre { get; set; }
	
	[Required]
	[Range(70, 600)]
	public uint Duration { get; set; }
}
