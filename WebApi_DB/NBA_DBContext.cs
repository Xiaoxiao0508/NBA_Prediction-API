using System;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi_DB
{
    // The database context is the main class that coordinates Entity Framework functionality for a data model.
    //  This class is created by deriving from the Microsoft.EntityFrameworkCore.DbContext class.
    public class NBA_DBContext : DbContext
    {
        // the property name need to map the database table
        // A DbSet represents the collection of all entities in the context, or that can be queried from the database,
        //  of a given type. DbSet objects are created from a DbContext using the DbContext.Set method.
        public DbSet<Player> allPlayers { get; set; }

        public DbSet<Team> Team { get; set; }

        public DbSet<ColumnHeaders> columnHeaders { get; set; }
        public DbSet<PlayerSelection> PlayerSelection{get;set;}

        public NBA_DBContext(DbContextOptions<NBA_DBContext> options) : base(options)
        {

        }

        // by default,a property named Id or <type name>Id will be configured as the primary key of an entity.
        // configure the composite key and other property name as a key in the following method

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Player>().HasKey(p=>new{
                p.Player_key
            });
            builder.Entity<Team>().HasKey(t => new
            {
                t.TeamName
            });

            builder.Entity<ColumnHeaders>().HasNoKey();
              builder.Entity<PlayerSelection>().HasKey(p => new
            {
                p.TeamName,p.Player_key
            });
        }

    }

}
