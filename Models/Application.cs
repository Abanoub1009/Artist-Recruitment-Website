using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Application
    {
        [Key]
        public int ApplicationId { get; set; }
        
        public int ArtistId { get; set; }
        public string CoverLetter { get; set; }
        public string PortfolioSampleUri { get; set; }
        public string Status { get; set; }
        public DateTime AppliedAt { get; set; }
        [ForeignKey("ArtistprofileId")]
        public ArtistProfile ArtistProfile { get; set; }
        public int ArtistprofileId { get; set; }
        public JobPost JobPost { get; set; }
    }
}
