using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Interface
{
    public interface IRecruiterProfileService
    {
        Task<List<RecruiterProfile>> GetAllAsync();
        Task<RecruiterProfile> GetByIdAsync(int id);
        Task AddAsync(RecruiterProfile recruiterProfile);
        Task EditAsync(RecruiterProfile recruiterProfile);
        Task DeleteAsync(int id);
    }
}
