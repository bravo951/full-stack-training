using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using Infrastructure.Repositories;
using static ApplicationCore.Models.FavoriteResponseModel;
using System.Linq;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IReviewRepository _reviewRepository;


        public UserService(IUserRepository userRepository, IFavoriteRepository favoriteRepository, 
            IMovieRepository movieRepository, IPurchaseRepository purchaseRepository, IReviewRepository reviewRepository)
        {
            _userRepository = userRepository;
            _favoriteRepository = favoriteRepository;
            _movieRepository = movieRepository;
            _purchaseRepository = purchaseRepository;
            _reviewRepository = reviewRepository;

        }

        public async Task<int> RegisterUser(UserRegisterRequestModel requestModel)
        {
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);
            if (dbUser != null)
            {
                throw new Exception("Email already exists, please login");
            }
            var salt = GetSalt();
            var hashedPassword = GetHashedPassword(requestModel.Password, salt);
            var user = new User()
            {
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                Email = requestModel.Email,
                Salt = salt,
                HashedPassword = hashedPassword,
                DateOfBirth = requestModel.DateOfBirth
            };
            var newUser = await _userRepository.Add(user);
            return newUser.Id;
        }
        public async Task<UserLoginResponseModel> LoginUser(UserLoginRequestModel requestModel)
        {
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);

            var hashedPassword = GetHashedPassword(requestModel.Password, dbUser.Salt);
            if(hashedPassword == dbUser.HashedPassword)
            {
                var userLoginResponseModel = new UserLoginResponseModel
                {
                    Id = dbUser.Id,
                    FirstName = dbUser.FirstName,
                    LastName = dbUser.LastName,
                    DateOfBirth = dbUser.DateOfBirth.GetValueOrDefault(),
                    Email = dbUser.Email
                };
                return userLoginResponseModel;
            }
            return null;
        }
        private string GetSalt()
        {
            var randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }
        private string GetHashedPassword(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                Convert.FromBase64String(salt),
                KeyDerivationPrf.HMACSHA512,
                10000,
                256 / 8));
            return hashed;
        }

        public async Task AddFavorite(FavoriteResposeModel favoriteRequest)
        {
            var favorite = new Favorite
            {
                MovieId = favoriteRequest.MovieId,
                UserId = favoriteRequest.UserId
            };
            await _favoriteRepository.Add(favorite);
        }

        public async Task RemoveFavorite(FavoriteResposeModel favoriteRequest)
        {
            var favorite = new Favorite
            {
                MovieId = favoriteRequest.MovieId,
                UserId = favoriteRequest.UserId
            };
            await _favoriteRepository.Delete(favorite);
        }

        public async Task<FavoriteResponseModel> GetAllFavoritesForUser(int id)
        {
            var favorites = await _favoriteRepository.Get(f => f.UserId == id);
            var favoriteList = new List<FavoriteMovieResponseModel>();
            foreach (var item in favorites)
            {
                var movie = await _movieRepository.GetMovieById(item.MovieId);
                favoriteList.Add(new FavoriteMovieResponseModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    PosterUrl = movie.PosterUrl
                });
            }
            var favoriteResponse = new FavoriteResponseModel
            {
                UserId = id,
                FavoriteMovies = favoriteList
            };
            return favoriteResponse;
        }

        public async Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {
            var movie = await _movieRepository.GetMovieById(purchaseRequest.MovieId);
            if(movie == null)
            {
                return false;
            }
            var purchase = new Purchase
            {
                UserId = userId,
                MovieId = purchaseRequest.MovieId,
                PurchaseNumber = purchaseRequest.PurchaseNumber.GetValueOrDefault(),
                PurchaseDateTime = purchaseRequest.PurchaseDateTime.GetValueOrDefault(),
                TotalPrice = movie.Price.GetValueOrDefault()
            };
            await _purchaseRepository.Add(purchase);
            return true;
        }

        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
        {
            var purchases = await _purchaseRepository.Get(p => p.UserId == userId && p.MovieId == purchaseRequest.MovieId);
            return purchases != null;
        }

        public async Task<PurchaseResponseModel> GetAllPurchasesForUser(int id)
        {
            var purchases = await _purchaseRepository.Get(p => p.UserId == id);
            var purchaseList = new List<MovieCardResponseModel>();
            foreach (var item in purchases)
            {
                var movie = await _movieRepository.GetMovieById(item.MovieId);
                purchaseList.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    PosterUrl = movie.PosterUrl
                });
            }
            var pr = new PurchaseResponseModel
            {
                UserId = id,
                PurchasedMovies = purchaseList
            };
            return pr;
        }

        public async Task<PurchaseDetailsResponseModel> GetPurchasesDetails(int userId, int movieId)
        {
            var purchase = (await _purchaseRepository.Get(p => p.UserId == userId && p.MovieId == movieId)).FirstOrDefault();
            var movie = await _movieRepository.GetMovieById(movieId);
            var purchaseDetail = new PurchaseDetailsResponseModel
            {
                Id = purchase.Id,
                UserId = userId,
                MovieId = movieId,
                PurchaseDateTime = purchase.PurchaseDateTime,
                PurchaseNumber = purchase.PurchaseNumber,
                TotalPrice = purchase.TotalPrice,
                Title = movie.Title,
                PosterUrl = movie.PosterUrl,
                ReleaseDate = movie.ReleaseDate.GetValueOrDefault()
            };
            return purchaseDetail;
        }

        public async Task AddMovieReview(ReviewRequestModel reviewRequest)
        {
            var review = new Review
            {
                UserId = reviewRequest.UserId,
                MovieId = reviewRequest.MovieId,
                ReviewText = reviewRequest.ReviewText,
                Rating = reviewRequest.Rating
            };
            await _reviewRepository.Add(review);
        }

        public async Task UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            var review = new Review
            {
                UserId = reviewRequest.UserId,
                MovieId = reviewRequest.MovieId,
                ReviewText = reviewRequest.ReviewText,
                Rating = reviewRequest.Rating
            };
            await _reviewRepository.Update(review);
        }

        public async Task DeleteMovieReview(int userId, int movieId)
        {
            var review = (await _reviewRepository.Get(r => r.UserId == userId && r.MovieId == movieId)).FirstOrDefault();
            await _reviewRepository.Delete(review);
        }

        public async Task<MovieDetailsResponseModel.UserReviewResponseModel> GetAllReviewsByUser(int id)
        {
            var reviews = await _reviewRepository.Get(r => r.UserId == id);
            var reviewList = new List<MovieDetailsResponseModel.MovieReviewResponseModel>();
            foreach (var item in reviews)
            {
                var movie = await _movieRepository.GetMovieById(item.MovieId);
                reviewList.Add(new MovieDetailsResponseModel.MovieReviewResponseModel
                {
                    UserId = id,
                    MovieId = item.MovieId,
                    ReviewText = item.ReviewText,
                    Rating =item.Rating,
                    Name = movie.Title
                });
            }
            var r = new MovieDetailsResponseModel.UserReviewResponseModel
            {
                UserId = id,
                MovieReviews = reviewList
            };
            return r;
        }
    }
}
