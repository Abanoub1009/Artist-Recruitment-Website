using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PortfolioItem
    {
        public int Id { get; set; }
        [Required, MaxLength(200)]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string MediaUrl { get; set; }
        [Required]
        public string Type { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.Now;
        [Required]
        public int ArtistProfileId { get; set; }
        public ArtistProfile ArtistProfile { get; set; }
    }
}
