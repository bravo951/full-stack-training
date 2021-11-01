using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MovieShopDbContext _dbContext;
        public UserRepository(MovieShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> AddUser(User user)
        {
            //
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }


        public async Task<User> GetUserByEmail(string Email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == Email);
            return user;
        }
    }
}
