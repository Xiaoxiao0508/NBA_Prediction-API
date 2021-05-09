using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace NBA_API.Models
{

    public class NBA_DBContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Player> allPlayers { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<ColumnHeaders> columnHeaders { get; set; }
        public DbSet<PlayerSelection> PlayerSelection { get; set; }

        public NBA_DBContext(DbContextOptions<NBA_DBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // we have to call the base first  becauee we are inheriting from IdentityDbContext<ApplicationUser> not DbContext
            base.OnModelCreating(builder);
            builder.Entity<Player>().HasKey(p => new
            {
                p.Player_key
            });
            builder.Entity<Team>().HasKey(t => new
            {
                t.TeamName,
                t.Id
            });

            builder.Entity<ColumnHeaders>().HasNoKey();
            builder.Entity<PlayerSelection>().HasKey(p => new
            {
                p.TeamName,
                p.Player_key,
                p.Id
            });
            builder.Entity<AspNetUsers>().HasKey(p=>new{
                p.Id
            });
        }

    }

}
