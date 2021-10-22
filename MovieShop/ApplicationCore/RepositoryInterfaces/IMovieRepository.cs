using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetTop30RevenueMovies();
    }
}
