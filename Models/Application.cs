using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Application
    {
        public int ApplicationId { get; set; }
        public int JobPostId { get; set; }
        public int ArtistId { get; set; }
        public string CoverLetter { get; set; }
        public string PortfolioSampleUri { get; set; }
        public string Status { get; set; } // "Pending", "Shortlisted", etc.
        public DateTime AppliedAt { get; set; }

        public ArtistProfile ArtistProfile { get; set; }
        public int ArtistprofileId { get; set; }
    }
}
