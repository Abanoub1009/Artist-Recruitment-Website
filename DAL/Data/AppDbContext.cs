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
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>

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
        public DbSet<RecruiterProfile> RecruiterProfiles { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Rename Identity tables (optional)
            builder.Entity<User>().ToTable("Users");

            // Remove IdentityUserToken table
            builder.Ignore<IdentityUserToken<int>>();

            // Application - ArtistProfile
            builder.Entity<Application>()
                .HasOne(a => a.ArtistProfile)
                .WithMany(ap => ap.Applications)
                .HasForeignKey(a => a.ArtistprofileId)
                .OnDelete(DeleteBehavior.Cascade);

            // Application - JobPost
            builder.Entity<Application>()
                .HasOne(a => a.JobPost)
                .WithOne(j => j.Application)
                .HasForeignKey<Application>(a => a.JobPostId)
                .OnDelete(DeleteBehavior.Restrict);

            // JobPost - RecruiterProfile
            builder.Entity<JobPost>()
                .HasOne(j => j.RecruiterProfile)
                .WithMany(rp => rp.JobPosts)
                .HasForeignKey(j => j.RecruiterProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            // ArtistProfile - User
            builder.Entity<ArtistProfile>()
                .HasOne(ap => ap.User)
                .WithOne(u => u.ArtistProfile)
                .HasForeignKey<ArtistProfile>(ap => ap.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // RecruiterProfile - User
            builder.Entity<RecruiterProfile>()
                .HasOne(rp => rp.User)
                .WithOne(u => u.RecruiterProfile)
                .HasForeignKey<RecruiterProfile>(rp => rp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Message - User
            builder.Entity<Message>()
                .HasOne(m => m.User)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasPrincipalKey(u => u.Id);

            // Review - User
            builder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }







    }
}