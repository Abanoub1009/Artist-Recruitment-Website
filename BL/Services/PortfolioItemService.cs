using BL.Services.Interface;
using DAL.Repository;
using DAL.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class PortfolioItemService : IPortfolioItemService
    {
        private readonly IPortfolioItemRepository _portfolioItemRepository;
        public PortfolioItemService(IPortfolioItemRepository portfolioItemRepository)
        {
            _portfolioItemRepository = portfolioItemRepository;
        }

        public async Task AddAsync(PortfolioItem portfolioItem)
        {
            await _portfolioItemRepository.AddAsync(portfolioItem);

        }

        public async Task DeleteAsync(int id)
        {
            var portfolioItem = await _portfolioItemRepository.GetByIdAsync(id);
            if (portfolioItem != null)
            {
                await _portfolioItemRepository.DeleteAsync(portfolioItem);
            }
        }

        public async Task<IEnumerable<PortfolioItem>> GetAllAsync()
        {
            return await _portfolioItemRepository.GetAllAsync();
        }

        public async Task<PortfolioItem> GetByIdAsync(int id)
        {
            return await _portfolioItemRepository.GetByIdAsync(id);
        }

        public async Task<PortfolioItem> UpdateStatusAsync(PortfolioItem portfolioItem)
        {
            await _portfolioItemRepository.UpdateAsync(portfolioItem);
            return portfolioItem;
        }
    }
}