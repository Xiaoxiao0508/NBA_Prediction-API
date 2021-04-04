using System;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi_DB
{
    public class NBA_DBContext:DbContext
    {
    
         public NBA_DBContext(DbContextOptions<NBA_DBContext> options)
            : base(options)
        {
        }
        // the property name need to map the database table
        public DbSet<Player> Player { get; set; }

        public DbSet<Team> Team{ get; set; }
        

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
