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

        [Url]
        public string ProfileImage { get; set; }

        [Url]
        public string CoverImage { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public ICollection<Message> SentMessages { get; set; }
        public ICollection<Message> ReceivedMessages { get; set; }

        // One-to-many: Portfolio items
        public ICollection<PortfolioItem> PortfolioItems { get; set; }

        public ICollection<Review> Reviews { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<BlogPost> BlogPosts { get; set; }
        public ICollection<JobPost> JobPosts { get; set; }

        // Adding SignalR - Track online users (if needed, you can add an online flag)
        // Optional: This is a signal that might be useful if you want to track whether a user is actively online
        public bool IsOnline { get; set; }  // This is optional, based on your app needs
    }
}
