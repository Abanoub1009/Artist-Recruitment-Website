using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        [ForeignKey("JobPost")]
        public int JobPostId { get; set; }
        public JobPost JobPost { get; set; }

    }
}