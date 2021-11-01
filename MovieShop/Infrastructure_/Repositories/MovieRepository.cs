using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        public MovieShopDbContext _dbContext { get; private set; }
        public MovieRepository(MovieShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<Movie> GetMovieById(int id)
        {
            var movie = await _dbContext.Movies.Include(m => m.Casts).ThenInclude(mc => mc.Cast)
                                         .Include(m => m.Genres).ThenInclude(mg => mg.Genre)
                                         .Include(m => m.Trailers).Include(m=>m.Reviews).
                                         SingleOrDefaultAsync(m => m.Id == id);
            movie.Rating = await _dbContext.Reviews.Where(r => r.MovieId == id).Select(r => r.Rating).AverageAsync();
            return movie;
                
        }
        public async Task<IEnumerable<Movie>> GetTop30RevenueMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return movies;
        }
    }
}
