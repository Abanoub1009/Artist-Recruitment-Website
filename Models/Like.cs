using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Like
    {
        public int LikeId { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
        [ForeignKey("JobPost")]
        public int JobPostId { get; set; }
        public JobPost  JobPost { get; set; }

    }
}