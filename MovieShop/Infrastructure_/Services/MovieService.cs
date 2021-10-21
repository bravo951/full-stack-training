using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        public List<MovieCardResponseModel> GetTop30RevenueMovies()
        {
            var movieCards = new List<MovieCardResponseModel>
            {
                new MovieCardResponseModel{Id = 1,Title = "Interception",PosterUrl = "https://image.tmdb.org/t/p/w342//9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg"},
                new MovieCardResponseModel{Id = 2,Title = "Interstellar",PosterUrl = "https://image.tmdb.org/t/p/w342//gEU2QniE6E77NI6lCU6MxlNBvIx.jpg"},
                new MovieCardResponseModel{Id = 3,Title = "The Dark Knight",PosterUrl = "https://image.tmdb.org/t/p/w342//qJ2tW6WMUDux911r6m7haRef0WH.jpg"}
            };
            return movieCards;
        }
    }
}
