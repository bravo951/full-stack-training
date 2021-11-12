﻿using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class FavoriteRepository : EfRepository<Favorite>,IFavoriteRepository
    {
        public FavoriteRepository(MovieShopDbContext dbContext) : base(dbContext) { }

    }
}
