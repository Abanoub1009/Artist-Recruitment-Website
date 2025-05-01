using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class JobPost
    {


        public int JobPostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SkillsRequired { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public string PayRate { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime PostedAt { get; set; }





        public Application Application { get; set; }
        public Application ApplicationId { get; set; }

        public RecruiterProfile Profile { get; set; }
        public int RecruiterId { get; set; }
    }
}
