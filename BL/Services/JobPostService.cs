using BL.Services.Interface;
using DAL.Repository.IRepository;
using DAL.UnitOfWork;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class JobPostService : IJobPostService
    {
        private readonly IJobPostRepository _jobPostRepository;
        private readonly IUnitOfWork _unitOfWork;
        public JobPostService(IJobPostRepository jobPostRepository, IUnitOfWork unitOfWork)
        {
            _jobPostRepository = jobPostRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddJobPostAsync(JobPost jobPost)
        {
            await _jobPostRepository.AddAsync(jobPost);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteJobPostByIdAsync(int id)
        {
            var jopPost = await _jobPostRepository.GetByIdAsync(id);
            if (jopPost != null)
            {
                _jobPostRepository.DeleteAsync(jopPost);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task<IEnumerable<JobPost>> GetAllJobPostsAsync()
        {
            return await _jobPostRepository.GetAllAsync();
        }

        public async Task<JobPost> GetJobPostByIdAsync(int id)
        {
            return await _jobPostRepository.GetByIdAsync(id);
        }

        public async Task<JobPost> UpdateJobPostAsync(JobPost jobPost)
        {
            await _jobPostRepository.UpdateAsync(jobPost);
            await _unitOfWork.SaveAsync();
            return jobPost;
        }
    }
}