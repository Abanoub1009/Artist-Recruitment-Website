using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<BlogPost> Blogs { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<ArtistProfile> Artists { get; set; }
        public DbSet<PortfolioItem> Items { get; set; }
    }
}
