using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IMovieRepository : IAsyncRepository<Movie>
    {
        public Task<IEnumerable<Movie>> GetTop30RevenueMovies();
        public Task<Movie> GetMovieById(int id);
        public Task<IEnumerable<Movie>> GetMovieByGenre(int id, int pageSize, int pageIndex);
        public Task<IEnumerable<Review>> GetMovieReviews(int id, int pageSize, int page);
        public Task<IEnumerable<Object>> GetPurchasedMovie(int pageSize, int pageIndex);
    }
}
