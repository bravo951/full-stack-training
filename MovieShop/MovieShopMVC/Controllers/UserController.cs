using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Services;

namespace MovieShopMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserService _userService;

        public UserController(ICurrentUserService currentUserService, IUserService userService)
        {
            _currentUserService = currentUserService;
            _userService = userService;
        }

        //all for when after authentification
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Purchase(int id)
        {
            var userId = _currentUserService.UserId;
            PurchaseRequestModel request = new PurchaseRequestModel { MovieId = id };
            await _userService.PurchaseMovie(request, userId);
            //var movieId = User.
            //if (userIdentity != null && userIdentity.IsAuthenticated)
            //{
            //    return View();
            //}
            //RedirectToAction("Login", "Account");
            return Content("<script>alert('Purchased Successfully');</script>");
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Purchases()
        {
            //get all movies purchased by a user
            var userId = _currentUserService.UserId;
            var purchases = await _userService.GetAllPurchasesForUser(userId);
            return View(purchases);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Favorite()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Favorites(int id)
        {
            //get all movies liked by a user
            var userId = _currentUserService.UserId;
            var favorites = await _userService.GetAllFavoritesForUser(userId);
            return View(favorites);
        }

        [HttpPost]
        public async Task<IActionResult> Reviews(ReviewRequestModel reviewRequest,int movieId)
        {
            reviewRequest.UserId = _currentUserService.UserId;
            reviewRequest.MovieId = movieId;
            await _userService.AddMovieReview(reviewRequest);
            return Content("<script>alert('Thanks For Your Comment');</script>");
        }

        [HttpGet]
        public async Task<IActionResult> Reviews(int id)
        {
            return View("review");
        }
    }
}