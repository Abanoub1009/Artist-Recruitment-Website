using DAL.Data;
using DAL.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        readonly private AppDbContext _context;
        public ReviewRepository(AppDbContext context) : base(context)
        {
            _context = context;

        }
    }
}