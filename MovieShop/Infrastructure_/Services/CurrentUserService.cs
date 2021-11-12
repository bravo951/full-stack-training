using Microsoft.AspNetCore.Http;
using MovieShopMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        //user httpcontext class to get all info
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string temp => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        public int UserId => Convert.ToInt32(_httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User.Identity!= null &&
                                       _httpContextAccessor.HttpContext != null &&
                                        _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        public string FullName => _httpContextAccessor.HttpContext?.User.Claims
                                    .FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value
                                    + " " + _httpContextAccessor.HttpContext?.User.Claims
                                    .FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;

        public string Email => _httpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        public IEnumerable<string> Roles { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime DateOfBirth  => Convert.ToDateTime(_httpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.DateOfBirth)?.Value);
    }
}
