using BL.Services.Interface;
using DAL.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IBlogPostRepository _repo;
        public BlogPostService(IBlogPostRepository repo)
        {
            _repo = repo;
        }

        public async Task AddAsync(BlogPost blogPost)
        {
            await _repo.AddAsync(blogPost);
        }

        public async Task DeleteAsync(int id)
        {
            var Bpost = await _repo.GetByIdAsync(id);
            if (Bpost != null)
                await _repo.DeleteAsync(Bpost);
        }

        public async Task EditAsync(BlogPost blogPost)
        {
            await _repo.UpdateAsync(blogPost);
        }

        public async Task<List<BlogPost>> GetAllAsync()
        {
            var Bposts = await _repo.GetAllAsync();
            return Bposts.ToList();
        }

        public async Task<BlogPost> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }
    }
}
