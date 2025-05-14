using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        public string? Username { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("BlogPost")]
        public int BlogPostId { get; set; }
        public virtual BlogPost? BlogPost { get; set; }

        [ForeignKey("ArtistProfile")]
        public int? ArtistProfileId { get; set; }
        public virtual ArtistProfile? ArtistProfile { get; set; }
        // You do NOT need to add a ProfileImage property here.
        // Access it via: comment.ArtistProfile.ProfileImage
    }
}