using MoviesApi.Data.Dtos;
using MoviesApi.Data.Models;
using AutoMapper;


namespace MoviesApi.Data.Profiles;


public class MovieProfile : Profile
{
	public MovieProfile()
	{
		CreateMap<CreateMovieDto, Movie>();
	}
}
