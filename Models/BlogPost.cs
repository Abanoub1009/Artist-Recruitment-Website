using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BlogPost
    {
        [Key]
        public int BlogPostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        [ForeignKey("ArtistProfile")]
        public int PostedBy { get; set; } // FK to Admin (User)
        public DateTime PostedAt { get; set; } = DateTime.Now;


        public virtual ArtistProfile? ArtistProfile { get; set; }
        public virtual ICollection<Like>? Likes { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }

    }
}
