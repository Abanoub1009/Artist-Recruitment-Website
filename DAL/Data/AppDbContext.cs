using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class AppDbContext : IdentityDbContext<User,Role,int>

    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<BlogPost> Blogs { get; set; }
        public DbSet<ArtistProfile> ArtistProfiles { get; set; }
        public DbSet<PortfolioItem> PortfolioItems { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Like>()
                .HasOne(l => l.BlogPost)
                .WithMany(b => b.Likes)
                .HasForeignKey(l => l.BlogPostId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.ArtistProfile)
                .WithMany(a => a.Likes)
                .HasForeignKey(l => l.ArtistprofileId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure Comment relationships
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.BlogPost)
                .WithMany(b => b.Comments)
                .HasForeignKey(c => c.BlogPostId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ArtistProfile)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.ArtistProfileId)
                .OnDelete(DeleteBehavior.NoAction);
        }

    }
}