using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }
        [ForeignKey("ArtistProfile")]
        public int ArtistProfileId { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ArtistProfile ArtistProfile { get; set; }

    }
}
