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
        public string YoutubeLink { get; set; }

        [Url]
        public string InstagramLink { get; set; }

        [Url]
        public string FacebookLink { get; set; }

        [Url]
        public string ShowreelLink { get; set; }

        [Url]
        public string ProfileImage { get; set; }

        [Url]
        public string CoverImage { get; set; }

        // Foreign key for User
        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

        // One-to-many: Portfolio items
        public ICollection<PortfolioItem> PortfolioItems { get; set; }
        public ICollection<Application> Applications { get; set; }
    }
}
