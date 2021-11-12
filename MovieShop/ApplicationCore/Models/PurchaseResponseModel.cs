using System.Collections.Generic;

namespace ApplicationCore.Models
{
    public class PurchaseResponseModel
    {
        public int UserId { get; set; }
        public int TotalMoviesPurchased { get; set; }
        public List<MovieCardResponseModel> PurchasedMovies { get; set; }
    }
}