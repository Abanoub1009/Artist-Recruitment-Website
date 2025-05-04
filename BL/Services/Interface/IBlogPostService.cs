using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Interface
{
    public interface IBlogPostService
    {
        Task<List<BlogPost>> GetAllAsync();
        Task<BlogPost> GetByIdAsync(int id);
        Task AddAsync(BlogPost blogPost);
        Task EditAsync(BlogPost blogPost);
        Task DeleteAsync(int id);
    }
}
