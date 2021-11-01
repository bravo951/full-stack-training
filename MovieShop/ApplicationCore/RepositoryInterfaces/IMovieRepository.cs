using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetTop30RevenueMovies();
        Task<Movie> GetMovieById(int id);
    }
}
