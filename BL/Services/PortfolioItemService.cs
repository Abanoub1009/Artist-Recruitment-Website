using BL.Services.Interface;
using DAL.Repository;
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
    public class PortfolioItemService : IPortfolioItemService
    {
        private readonly IPortfolioItemRepository _portfolioItemRepository;
        private readonly IUnitOfWork _unitOFWork;
        public PortfolioItemService(IPortfolioItemRepository portfolioItemRepository, IUnitOfWork unitOFWork)
        {
            _portfolioItemRepository = portfolioItemRepository;
            _unitOFWork = unitOFWork;
        }

        public async Task AddAsync(PortfolioItem portfolioItem)
        {
            await _portfolioItemRepository.AddAsync(portfolioItem);
            await _unitOFWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var portfolioItem = await _portfolioItemRepository.GetByIdAsync(id);
            if (portfolioItem != null)
            {
                _portfolioItemRepository.DeleteAsync(portfolioItem);
                await _unitOFWork.SaveAsync();
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
            await _unitOFWork.SaveAsync();
            return portfolioItem;
        }
    }
}