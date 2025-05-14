using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PortfolioItem
    {
        [Key]
        public int PortfolioItemId { get; set; }
        [Required, MaxLength(200)]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string MediaUrl { get; set; }
        [Required]
        public string Type { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.Now;
        public int ArtistProfileId { get; set; }
        [ForeignKey("ArtistProfileId")]
        public virtual ArtistProfile? ArtistProfile { get; set; }
    }
}