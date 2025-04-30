using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Recruiter
    {
        public int RecruiterId { get; set; }
        [MaxLength(1000)]
        public string Bio { get; set; }
        [MaxLength(500)]
        public string Skills { get; set; }
        public string Location { get; set; }
        public bool IsAvailable { get; set; }
        [Url]
        public string YoutubeLink { get; set; }
        [Url]
        public string InstgramLink { get; set; }
        [Url]
        public string FacebookLink { get; set; }
        [Url]
        public string ShowreelLink { get; set; }
        [Url]
        public string ProfileImage { get; set; }
        [Url]
        public string CoverIamge { get; set; }
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<PortfolioItem> portfolioItems { get; set; }
    }
}
