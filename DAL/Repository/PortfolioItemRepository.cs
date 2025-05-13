using DAL.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class PortfolioItemRepository : Repository<PortfolioItem>, IPortfolioItemRepository
    {
        private readonly AppDbContext _context;
        public PortfolioItemRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}