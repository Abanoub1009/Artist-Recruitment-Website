using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class RecruiterProfile
    {
        [Key]
        public int RecruiterProfileId { get; set; }

        [MaxLength(1000)]
        public string Bio { get; set; }

        [MaxLength(500)]
        public string Skills { get; set; }

        public string Location { get; set; }

        public bool IsAvailable { get; set; }

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

        // Foreign key to User
        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

        // Recruiters don't need portfolio items typically, but if you want to keep it:
        public ICollection<PortfolioItem> PortfolioItems { get; set; }

        // One-to-many relationship with JobPost
        public ICollection<JobPost> JobPosts { get; set; }
    }
}
