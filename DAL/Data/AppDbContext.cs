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

        public DbSet<Application> Applications { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<BlogPost> Blogs { get; set; }
        public DbSet<ArtistProfile> ArtistProfiles { get; set; }
        public DbSet<PortfolioItem> PortfolioItems { get; set; }
        public DbSet<JobPost> JobPosts { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(ap => ap.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict); // Avoid circular cascade delete

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(ap => ap.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<JobPost>()
                .HasOne(j => j.ArtistProfile)
                .WithMany(a => a.JobPosts)
                .HasForeignKey(j => j.ArtistProfileId)
                .OnDelete(DeleteBehavior.Restrict); // or .NoAction
        }


    }
}