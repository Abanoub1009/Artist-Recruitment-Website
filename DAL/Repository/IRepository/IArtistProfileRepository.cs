using DAL.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IArtistProfileRepository : IRepository<ArtistProfile>
    {
        Task<ArtistProfile?> GettByIdAsync(int id);
    }
}
