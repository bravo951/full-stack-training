using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class UserController : Controller
    {
        //all for when after authentification
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Purchase()
        {
            var userIdentity = this.User.Identity;
            if (userIdentity != null && userIdentity.IsAuthenticated)
            {
                return View();
            }
            RedirectToAction("Login", "Account");

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Purchases(int id)
        {
            //get all movies purchased by a user

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Favorite()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Favorites(int id)
        {
            //get all movies liked by a user
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Reviews()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Reviews(int id)
        {
            return View();
        }
    }
}