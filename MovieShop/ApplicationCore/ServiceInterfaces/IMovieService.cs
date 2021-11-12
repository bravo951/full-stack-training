using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IMovieService
    {
        Task<List<MovieCardResponseModel>> GetTop30RevenueMovies();
        Task<List<MovieCardResponseModel>> GetMoviesByGenres(int id, int pageSize, int pageIndex);
        Task<MovieDetailsResponseModel> GetMovieDetails(int id);
        Task<IEnumerable<Object>> GetToppPuchasedMovies(int pageSize = 30, int pageIndex = 1);


    }
}
