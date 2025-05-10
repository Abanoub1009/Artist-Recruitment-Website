using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class UserContext : IdentityDbContext<User, Role, int>
    {
        public UserContext(DbContextOptions<UserContext> options):base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Application>()
                .HasOne(a => a.JobPost)
                .WithOne(j => j.Application)
                .HasForeignKey<Application>(a => a.JobPostId)
                .OnDelete(DeleteBehavior.Restrict); // prevent multiple cascade paths
        }

    }
}
