using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
