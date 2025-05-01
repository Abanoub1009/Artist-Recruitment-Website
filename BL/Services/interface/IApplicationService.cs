using Models.Models;

namespace BL.Services.Interface
{
    public interface IApplicationService
    {
        Task<List<Application>> GetAllAsync();
        Task<Application> GetByIdAsync(int id);
        Task AddAsync(Application application);
        Task UpdateStatusAsync(int id, string status);
        Task DeleteAsync(int id);
    }
}
