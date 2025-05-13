using DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public class UnitOFWork : IDisposable, IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOFWork(AppDbContext context)
        {
            _context = context;
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
