using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static ApplicationCore.Models.MovieDetailsResponseModel;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IUserService
    {
        Task<int> RegisterUser(UserRegisterRequestModel requestModel);
        Task<UserLoginResponseModel> LoginUser(UserLoginRequestModel user);
        Task AddFavorite(FavoriteResposeModel favoriteRequest);
        Task RemoveFavorite(FavoriteResposeModel favoriteRequest);
        Task<FavoriteResponseModel> GetAllFavoritesForUser(int id);

        Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId);
        Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId);
        Task<PurchaseResponseModel> GetAllPurchasesForUser(int id);
        Task<PurchaseDetailsResponseModel> GetPurchasesDetails(int userId, int movieId);

        Task AddMovieReview(ReviewRequestModel reviewRequest);
        Task UpdateMovieReview(ReviewRequestModel reviewRequest);
        Task DeleteMovieReview(int userId, int movieId);
        Task<UserReviewResponseModel> GetAllReviewsByUser(int id);
    }
}
