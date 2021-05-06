using System;
using Microsoft.EntityFrameworkCore;


namespace NBA_API.Models
{

    public class NBA_DBContext : DbContext
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
            builder.Entity<Player>().HasKey(p => new {
                p.Player_key
            });
            builder.Entity<Team>().HasKey(t => new
            {
                t.TeamName
            });

            builder.Entity<ColumnHeaders>().HasNoKey();
            builder.Entity<PlayerSelection>().HasKey(p => new
            {
                p.TeamName,
                p.Player_key
            });
        }

    }

}
