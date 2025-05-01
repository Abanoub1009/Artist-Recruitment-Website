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
    public class ApplicationService: IApplicationService
    {
        private readonly IApplicationRepository _repo;

        public ApplicationService(IApplicationRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Application>> GetAllAsync()
        {
            var result = await _repo.GetAllAsync();
            return result.ToList();
        }

        public async Task<Application> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task AddAsync(Application application)
        {
            application.Status = "Pending";
            application.AppliedAt = DateTime.UtcNow;
            await _repo.AddAsync(application);
        }

        public async Task UpdateStatusAsync(int id, string status)
        {
            var app = await _repo.GetByIdAsync(id);
            if (app == null) throw new Exception("Application not found");
            app.Status = status;
            await _repo.UpdateAsync(app);
        }

        public async Task DeleteAsync(int id)
        {
            var app = await _repo.GetByIdAsync(id);
            if (app != null)
                await _repo.DeleteAsync(app);
        }
    }
}
