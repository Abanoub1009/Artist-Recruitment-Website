using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Interface
{
    public interface IJobPostService
    {
        Task<IEnumerable<JobPost>> GetAllJobPostsAsync();
        Task<JobPost> GetJobPostByIdAsync(int id);
        Task AddJobPostAsync(JobPost jobPost);
        Task<JobPost> UpdateJobPostAsync(JobPost jobPost);
        Task DeleteJobPostByIdAsync(int id);
    }
}
