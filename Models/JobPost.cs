using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class JobPost
    {

        [Key]
        public int JobPostId { get; set; }
        public string? Title { get; set; }
        public string Description { get; set; }
        public string SkillsRequired { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public DateTime Deadline { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public int LikesCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Notification> Notifications { get; set; }





        [ForeignKey("ApplicationId")]
        public virtual Application Applications { get; set; }
        public int ApplicationId { get; set; }

        public virtual ArtistProfile ArtistProfile { get; set; }
        public int ArtistProfileId { get; set; }
    }
}