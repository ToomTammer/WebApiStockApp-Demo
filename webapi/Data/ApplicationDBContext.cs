using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using webapi.Model;

namespace webapi.Controllers.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContexOptions)
        : base(dbContexOptions)
        {
            
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }

         ///OnModelCreating method within a class that extends DbContext
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ///StartPoint Many To Many
            ///This code snippet is configuring relationships between entities using Fluent API in Entity Framework Core. 
            builder.Entity<Portfolio>(x => x.HasKey(p => new { p.AppUserId, p.StockId }));
             ///EndPoint Many To Many

            // Configuring the relationship between Portfolio and AppUser entities
            builder.Entity<Portfolio>()
                .HasOne(u => u.AppUser)            // Portfolio has one AppUser
                .WithMany(u => u.Portfolios)        // AppUser can have many Portfolios
                .HasForeignKey(p => p.AppUserId);  // Foreign key constraint on AppUserId

            // Configuring the relationship between Portfolio and Stock entities
            builder.Entity<Portfolio>()
                .HasOne(u => u.Stock)              // Portfolio has one Stock
                .WithMany(u => u.Portfolios)        // Stock can be associated with many Portfolios
                .HasForeignKey(p => p.StockId);    // Foreign key constraint on StockId
            
            // builder.Entity<Post>()
            // .HasOne(p => p.Blog)
            // .WithMany(b => b.Posts)
            // .HasForeignKey(p => p.BlogId)
            // .HasConstraintName("BlogForeignKey");
           
            

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Amin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                }

            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}