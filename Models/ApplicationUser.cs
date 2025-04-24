using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ApplicationUser : IdentityUser
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
    }
}
