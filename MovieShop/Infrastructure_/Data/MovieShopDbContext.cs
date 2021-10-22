using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public class MovieShopDbContext : DbContext
    {
        //get the connection string into ctr
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options)
        {


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<Cast>(ConfigureCast);
            modelBuilder.Entity<User>(ConfigureUser);
            modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
            modelBuilder.Entity<Review>(ConfigureReview);
            modelBuilder.Entity<Favorite>(ConfigureFavorite);
            modelBuilder.Entity<Purchase>(ConfigurePurchase);
            modelBuilder.Entity<UserRole>(ConfigureUserRole);
            modelBuilder.Entity<Crew>(ConfigureCrew);
            modelBuilder.Entity<MovieCrew>(ConfigureMovieCrew);
        }

        private void ConfigureUserRole(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole");
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });
            builder.HasOne(ur => ur.User).WithMany(u => u.Roles).HasForeignKey(ur => ur.UserId);
            builder.HasOne(ur => ur.Role).WithMany(r => r.Users).HasForeignKey(ur => ur.RoleId);
        }

        private void ConfigurePurchase(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchase");
            builder.HasKey(p => p.Id);
            builder.Property("TotalPrice").HasColumnType("decimal(18,2)").HasDefaultValue(9.9m);
            builder.HasOne(p => p.Movie).WithMany(m => m.Purchases).HasForeignKey(p => p.MovieId);
            builder.HasOne(p => p.User).WithMany(u => u.Purchases).HasForeignKey(p => p.MovieId);
        }

        private void ConfigureFavorite(EntityTypeBuilder<Favorite> builder)
        {
            builder.ToTable("Favotite");
            builder.HasKey(f => f.Id);
            builder.HasOne(f => f.Movie).WithMany(m => m.Favorites).HasForeignKey(f => f.MovieId);
            builder.HasOne(f => f.User).WithMany(u => u.Favorites).HasForeignKey(f => f.MovieId);
        }

        private void ConfigureReview(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Review");
            builder.HasKey(r => new { r.MovieId, r.UserId });
            builder.Property("Rating").HasColumnType("decimal(3,2)").HasDefaultValue(9.9m);
            builder.HasOne(r => r.Movie).WithMany(m => m.Reviews).HasForeignKey(r=>r.MovieId);
            builder.HasOne(r => r.User).WithMany(u => u.Reviews).HasForeignKey(r => r.UserId);
        }

        private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder)
        {
            builder.ToTable("MovieCast");
            builder.HasKey(mc => new { mc.Character,mc.MovieId, mc.CastId });
            builder.Property("Character").HasMaxLength(450);
            builder.HasOne(mc => mc.Movie).WithMany(m => m.Casts).HasForeignKey(mc=>mc.MovieId);
            builder.HasOne(mc => mc.Cast).WithMany(c => c.Movies).HasForeignKey(mc => mc.CastId);
        }
        private void ConfigureMovieCrew(EntityTypeBuilder<MovieCrew> builder)
        {
            builder.ToTable("MovieCrew");
            builder.HasKey(mc => new { mc.Department,mc.Job, mc.MovieId, mc.CrewId });
            builder.Property("Department").HasMaxLength(128);
            builder.Property("Job").HasMaxLength(128);
            builder.HasOne(mc => mc.Movie).WithMany(m => m.Crews).HasForeignKey(mc => mc.MovieId);
            builder.HasOne(mc => mc.Crew).WithMany(c => c.Movies).HasForeignKey(mc => mc.CrewId);
        }

        private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.ToTable("MovieGenre");
            builder.HasKey(mg => new { mg.MovieId, mg.GenreId });
            builder.HasOne(mg => mg.Movie).WithMany(m => m.Genres).HasForeignKey(mg => mg.MovieId);
            builder.HasOne(mg => mg.Genre).WithMany(g => g.Movies).HasForeignKey(mg => mg.GenreId);
        }

        private void ConfigureCast(EntityTypeBuilder<Cast> builder)
        {
            builder.ToTable("Cast");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Name).HasMaxLength(128);
            builder.Property(m => m.ProfilePath).HasMaxLength(2084);
        }
        private void ConfigureCrew(EntityTypeBuilder<Crew> builder)
        {
            builder.ToTable("Crew");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Name).HasMaxLength(128);
            builder.Property(m => m.ProfilePath).HasMaxLength(2084);
        }

        private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.FirstName).HasMaxLength(128);
            builder.Property(m => m.LastName).HasMaxLength(128);
            builder.Property(m => m.Email).HasMaxLength(256);
            builder.Property(m => m.HashedPassword).HasMaxLength(1024);
            builder.Property(m => m.Salt).HasMaxLength(1024);
            builder.Property(m => m.PhoneNumber).HasMaxLength(16);
            builder.Property(m => m.TwoFactorEnabled).HasDefaultValue(false);
            builder.Property(m => m.IsLocked).HasDefaultValue(false);
            builder.Property(m => m.AccessFailedCount).HasDefaultValue(0);

        }

        private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movie");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title).HasMaxLength(256);

            builder.Property(m => m.Overview).HasMaxLength(4096);
            builder.Property(m => m.Tagline).HasMaxLength(512);
            builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            builder.Property(m => m.PosterUrl).HasMaxLength(2084);
            builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            builder.Property(m => m.OriginalLanguage).HasMaxLength(64);
            builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
            builder.Property(m => m.Budget).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
            builder.Property(m => m.Revenue).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
            builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");
            //we wanna tell ef to ignore this prop and not create this column
            builder.Ignore(m => m.Rating);
        }

        //make sure entities classes are represented by dbsets
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cast> Casts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<MovieCast> MovieCasts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Crew> Crews { get; set; }
        public DbSet<MovieCrew> MovieCrews { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
