using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class ArtistProfile
    {
        [Key]
        public int ArtistProfileId { get; set; }

        [MaxLength(1000)]
        public string Bio { get; set; }

        [MaxLength(500)]
        public string Skills { get; set; }

        public string Location { get; set; }

        public bool IsAvailable { get; set; }

        [Range(50, 250)]
        public int? HeightInCm { get; set; }

        [Range(30, 200)]
        public int? WeightInKg { get; set; }

        [Url]
        public string? YoutubeLink { get; set; }

        [Url]
        public string? InstagramLink { get; set; }

        [Url]
        public string? FacebookLink { get; set; }

        [Url]
        public string? ShowreelLink { get; set; }
        public string ProfileImage { get; set; }
        public string CoverImage { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
        public virtual ICollection<BlogPost>? BlogPosts { get; set; }
        public virtual ICollection<Notification>? Notifications { get; set; }
        public virtual ICollection<PortfolioItem>? PortfolioItems { get; set; }
        public virtual ICollection<Like>? Likes { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }

        // Adding SignalR - Track online users (if needed, you can add an online flag)
        // Optional: This is a signal that might be useful if you want to track whether a user is actively online
        public bool IsOnline { get; set; }  // This is optional, based on your app needs
    }
}
