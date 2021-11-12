using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel requestModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var newUser = await _userService.RegisterUser(requestModel);
            return View("Login");
            //return to login page
        }
        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }
        //login method should vaidate eamil/password
        //put useauthentication middleware
        //inject cookie name,expiration time in configure service
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequestModel requestModel)
        {
            var user = await _userService.LoginUser(requestModel);
            if(user == null)
            {
                // username/password is wrong
                //show message it is wrong
                return View();
            }
            //create cookie
            //tell ASP.NET we will use cookie based authentication
            //claims: exp, username, pwd
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.NameIdentifier,Convert.ToString(user.Id)),
                new Claim(ClaimTypes.DateOfBirth,user.DateOfBirth.ToShortDateString()),
                new Claim("FullName",user.FirstName+" "+user.LastName)

            };
            //Identity
            //creating the cookie
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return LocalRedirect("~/");
            
            //return to login page
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

    }
}