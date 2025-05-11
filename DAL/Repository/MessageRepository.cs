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
    public class MessageRepository :Repository<Message> , IMessageRepository

    {
        private readonly AppDbContext  _context;
        public MessageRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
