using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Interface
{
    public interface IArtistProfileService
    {
        Task<List<ArtistProfile>> GetAllAsync();
        Task<ArtistProfile> GetByIdAsync(int id);
        Task AddAsync(ArtistProfile artistProfile);
        Task EditAsync(ArtistProfile artistProfile);
        Task DeleteAsync(int id);
    }
}
