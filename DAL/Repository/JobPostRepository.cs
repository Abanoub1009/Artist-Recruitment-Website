using DAL.Data;
using DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class JobPostRepository : Repository<JobPost>, IJobPostRepository
    {
        private readonly AppDbContext _context;
        public JobPostRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}