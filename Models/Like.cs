using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{

    public class Like
    {
        public int LikeId { get; set; }
        public int BlogPostId { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual BlogPost? BlogPost { get; set; }
        public virtual ArtistProfile? ArtistProfile { get; set; }
        public int ArtistprofileId { get; set; }
    }
}