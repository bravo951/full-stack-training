using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Models
{
    public class FavoriteResponseModel
    {
        public int UserId { get; set; }
        public List<FavoriteMovieResponseModel> FavoriteMovies { get; set; }
        public class FavoriteMovieResponseModel:MovieCardResponseModel
        {
            //MovieCardResponseModel movieCard;
        }
    }
    
}
