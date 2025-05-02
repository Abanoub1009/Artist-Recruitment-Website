using DAL.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ArtistProfileRepository : Repository<ArtistProfile>, IArtistProfileRepository
    {
        private readonly AppDbContext _context;
        public ArtistProfileRepository(AppDbContext context):base(context)
        {
            _context = context;
        }
    }
}
