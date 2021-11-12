using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static ApplicationCore.Models.MovieDetailsResponseModel;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<List<MovieCardResponseModel>> GetMoviesByGenres(int genreId, int pagesize = 30,int pageIndex = 1)
        {
            var movies = await _movieRepository.GetMovieByGenre(genreId,pagesize,pageIndex);
            var movieCards = new List<MovieCardResponseModel>();
            foreach (var item in movies)
            {
                movieCards.Add(new MovieCardResponseModel { Id = item.Id, Title = item.Title, PosterUrl = item.PosterUrl });

            }
            return movieCards;
        }
        public async Task<MovieDetailsResponseModel> GetMovieDetails(int id)
        {
            var movie = await _movieRepository.GetMovieById(id);
            if(movie == null)
            {
                throw new Exception($"No movie found for this {id}");
            }
            
            var movieDetails = new MovieDetailsResponseModel
            {
                
                Id = movie.Id,
                Budget = movie.Budget,
                Overview = movie.Overview,
                Price = movie.Price,
                PosterUrl = movie.PosterUrl,
                Revenue = movie.Revenue,
                ReleaseDate = movie.ReleaseDate.GetValueOrDefault(),
                Rating = movie.Rating,
                Tagline = movie.Tagline,
                Title = movie.Title,
                RunTime = movie.RunTime,
                BackdropUrl = movie.BackdropUrl,
                //FavoritesCount = favoritesCount,
                ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl
            };
            foreach(var genre in movie.Genres)
            {
                movieDetails.Genres.Add(new GenreModel() { Id = genre.GenreId, Name = genre.Genre.Name });
            }
            foreach (var cast in movie.Casts)
            {
                movieDetails.Casts.Add(new CastResponseModel() { Id = cast.CastId, Character=cast.Character,Name=cast.Cast.Name,ProfilePath=cast.Cast.ProfilePath });
            }
            foreach (var trailer in movie.Trailers)
            {
                movieDetails.Trailers.Add(new TrailerResponseModel() { Id = trailer.Id, Name = trailer.Name, TrailerUrl = trailer.TrailerUrl, MovieId = trailer.MovieId });
            }
            return movieDetails;
        }

        public async Task<List<MovieCardResponseModel>> GetTop30RevenueMovies()
        {
            var movies = await _movieRepository.GetTop30RevenueMovies();
            var movieCards = new List<MovieCardResponseModel>();
            foreach(var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel{Id = movie.Id,PosterUrl=movie.PosterUrl,Title = movie.Title});
            }
            return movieCards;
        }
        public async Task<IEnumerable<Object>> GetToppPuchasedMovies(int pageSize=30,int pageIndex=1)
        {
            var movies = await _movieRepository.GetPurchasedMovie(pageSize, pageIndex);
            return movies;
        }

    }
}
