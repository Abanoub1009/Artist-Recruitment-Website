using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Interface
{
    public interface IPortfolioItemService
    {
        Task<IEnumerable<PortfolioItem>> GetAllAsync();
        Task<PortfolioItem> GetByIdAsync(int id);
        Task AddAsync(PortfolioItem portfolioItem);
        Task<PortfolioItem> UpdateStatusAsync(PortfolioItem portfolioItem);
        Task DeleteAsync(int id);
    }
}