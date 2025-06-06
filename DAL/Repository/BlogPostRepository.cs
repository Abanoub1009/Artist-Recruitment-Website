﻿using DAL.Data;
using DAL.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class BlogPostRepository : Repository<BlogPost>, IBlogPostRepository
    {
        private readonly AppDbContext _context;
        public BlogPostRepository(AppDbContext context): base(context)
        {
            _context = context;
        }
    }
}
