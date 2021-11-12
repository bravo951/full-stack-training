using ApplicationCore.Entities;
using ApplicationCore.Models;
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
    public class UserRepository : EfRepository<User>,IUserRepository
    {
        //private readonly MovieShopDbContext _dbContext;
        public UserRepository(MovieShopDbContext dbContext):base(dbContext)
        {
        }

        public async Task<IEnumerable<Review>> GetReviewsByUser(int userId)
        {
            var reviews = await _dbContext.Reviews.Where(r => r.UserId == userId).ToListAsync();
            return reviews;
        }

        public async Task<User> GetUserByEmail(string Email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == Email);
            return user;
        }
        
    }
}
