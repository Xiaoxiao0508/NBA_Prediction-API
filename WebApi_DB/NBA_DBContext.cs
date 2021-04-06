using System;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi_DB
{
    // The database context is the main class that coordinates Entity Framework functionality for a data model.
    //  This class is created by deriving from the Microsoft.EntityFrameworkCore.DbContext class.
    public class NBA_DBContext:DbContext
    {
    
         public NBA_DBContext(DbContextOptions<NBA_DBContext> options)
            : base(options)
        {
        }
        // the property name need to map the database table
        // A DbSet represents the collection of all entities in the context, or that can be queried from the database,
        //  of a given type. DbSet objects are created from a DbContext using the DbContext.Set method.
        public DbSet<Player> Player { get; set; }

        public DbSet<Team> Team{ get; set; }
        
       // by default,a property named Id or <type name>Id will be configured as the primary key of an entity.
       // configure the composite key and other property name as a key in the following method
        protected override void OnModelCreating(ModelBuilder builder)
         {
            builder.Entity<Player>().HasKey(p => new {
            p.SEASON, p.PLAYER_ID
            });
            builder.Entity<Team>().HasKey(t => new {
             t.TeamName
            });
         }
         
     
        
    }

}
