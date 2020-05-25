using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportLeagueASPNetCoreTest.Models;

namespace SportLeagueASPNetCoreTest.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        /// <summary>
        /// Фильмы
        /// </summary>
        public DbSet<FilmInfo> Movies { get; set; }

        /// <summary>
        /// Картинки к фильмам
        /// </summary>
        public DbSet<PosterInfo> Posters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlite("Data Source=Movies.db");
    }
}
