using Microsoft.EntityFrameworkCore;
using MoviesApi.Data.Models;


namespace MoviesApi.Data.Context;


public class MoviesContext : DbContext
{
	public MoviesContext(DbContextOptions<MoviesContext> options) : base(options)
	{
		
	}
	
	public DbSet<Movie> Movies { get; set; }
}
