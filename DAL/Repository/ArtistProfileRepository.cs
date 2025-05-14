using DAL.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class ArtistProfileRepository : Repository<ArtistProfile>, IArtistProfileRepository
    {
        private readonly AppDbContext _context;
        public ArtistProfileRepository(AppDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<ArtistProfile?> GettByIdAsync(int id)
        {
            return await _context.ArtistProfiles
                .Include(ap => ap.User)
                .FirstOrDefaultAsync(ap => ap.ArtistProfileId == id);
        }
    }
}
