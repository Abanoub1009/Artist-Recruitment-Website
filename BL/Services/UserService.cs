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
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }
        public async Task<List<User>> GetAllAsync()
        {
            var result = await _repo.GetAllAsync();
            return result.ToList();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task AddAsync(User user)
        {
            await _repo.AddAsync(user);
        }


        public async Task DeleteAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user != null)
                await _repo.DeleteAsync(user);
        }

        public async Task EditAsync(User Updtateduser)
        {
            await _repo.UpdateAsync(Updtateduser);
        }

    }
}
