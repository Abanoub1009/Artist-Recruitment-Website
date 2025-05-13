using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime SentAt { get; set; } = DateTime.Now;

        // Foreign keys for sender and receiver
        [Required]
        public int SenderId { get; set; }


        public ArtistProfile Sender { get; set; }

        [Required]
        public int ReceiverId { get; set; }

        public ArtistProfile Receiver { get; set; }

        // Status of the message (read/unread)
        public bool IsRead { get; set; } = false;
    }
}
