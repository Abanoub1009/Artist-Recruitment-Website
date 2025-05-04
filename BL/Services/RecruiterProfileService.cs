using BL.Services.Interface;
using DAL.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class RecruiterProfileService : IRecruiterProfileService
    {
        private readonly IRecruiterProfileRepository _repo;
        public RecruiterProfileService(IRecruiterProfileRepository repo)
        {
            _repo = repo;
        }

        public async Task AddAsync(RecruiterProfile recruiterProfile)
        {
            await _repo.AddAsync(recruiterProfile);
        }

        public async Task DeleteAsync(int id)
        {
            var rec = await _repo.GetByIdAsync(id);
            if (rec != null)
                await _repo.DeleteAsync(rec);
        }

        public async Task EditAsync(RecruiterProfile recruiterProfile)
        {
            await _repo.UpdateAsync(recruiterProfile);
        }

        public async Task<List<RecruiterProfile>> GetAllAsync()
        {
            var recruters = await _repo.GetAllAsync();
            return recruters.ToList();
        }

        public async Task<RecruiterProfile> GetByIdAsync(int id)
        {
            var rec = await _repo.GetByIdAsync(id);
            return rec;
        }
    }
}
