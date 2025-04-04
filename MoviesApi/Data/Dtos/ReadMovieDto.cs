namespace MoviesApi.Data.Dtos;


public class ReadMovieDto
{
	public uint Id { get; set; }
	
	public string? Title { get; set; }
	
	public string? Genre { get; set; }
	
	public uint Duration { get; set; }

	public DateTime QueryTime { get; set; } = DateTime.Now;
}
