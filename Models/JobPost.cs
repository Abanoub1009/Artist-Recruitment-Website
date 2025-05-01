using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class JobPost
    {

        [Key]
        public int JobPostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SkillsRequired { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public string PayRate { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime PostedAt { get; set; }





        public virtual Application Application { get; set; }
        

        public virtual RecruiterProfile RecruiterProfile { get; set; }
        public int RecruiterProfileId { get; set; }
    }
}
