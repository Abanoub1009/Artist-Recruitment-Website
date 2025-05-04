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
    public class RecruiterProfileRepository : Repository<RecruiterProfile>, IRecruiterProfileRepository
    {
        private readonly AppDbContext _context;
        public RecruiterProfileRepository(AppDbContext context):base(context)
        {
            _context = context;
        }
    }
}
