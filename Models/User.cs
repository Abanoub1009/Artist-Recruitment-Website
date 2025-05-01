using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class User : IdentityUser<int>
    {
        [Required, MaxLength(100)]
        public string FullName { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        // One-to-one with ArtistProfile
        public virtual ArtistProfile ArtistProfile { get; set; }

        // One-to-one with RecruiterProfile
        public virtual RecruiterProfile RecruiterProfile { get; set; }

        // One-to-many relationships
        public ICollection<Message> Messages { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<BlogPost> BlogPosts { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
