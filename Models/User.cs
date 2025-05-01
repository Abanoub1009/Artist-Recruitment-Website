using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User : IdentityUser
    {
        [Required, MaxLength(100)]
        public string FullName { get; set; }
        [Required, EmailAddress]
        public override string? Email { get => base.Email; set => base.Email = value; }
        [Phone]
        public override string? PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }
        [Required]
        public string Role { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        public virtual ArtistProfile ArtistProfile { get; set; }
        public virtual RecruiterProfile RecruiterProfile { get; set; }
        public List<Message> Messages { get; set; }
        public List<Review> reviews { get; set; }
        public List<Notification> notifications { get; set; }
        public List<BlogPost> blogPosts { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
