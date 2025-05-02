using BL.Services.Interface;
using DAL.Repository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class ArtistProfileService : IArtistProfileService
    {
        private readonly IArtistProfileRepository _repo;
        public ArtistProfileService(IArtistProfileRepository repo)
        {
            _repo = repo;
        }

        public async Task AddAsync(ArtistProfile artistProfile)
        {
            await _repo.AddAsync(artistProfile);
        }

        public async Task DeleteAsync(int id)
        {
            var artist = await _repo.GetByIdAsync(id);
            if (artist != null)
                await _repo.DeleteAsync(artist);
        }

        public async Task EditAsync(ArtistProfile artistProfile)
        {
            await _repo.UpdateAsync(artistProfile);
        }

        public async Task<List<ArtistProfile>> GetAllAsync()
        {
            var artists = await _repo.GetAllAsync();
            return artists.ToList();
        }

        public async Task<ArtistProfile> GetByIdAsync(int id)
        {
            var artist = await _repo.GetByIdAsync(id);
            return artist;
        }
    }
}
