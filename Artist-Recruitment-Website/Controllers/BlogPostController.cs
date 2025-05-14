using BL.Services.Interface;
using DAL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Artist_Recruitment_Website.Controllers
{
    [Authorize]
    public class BlogPostController : Controller
    {
        private readonly IBlogPostService _service;
        private readonly AppDbContext _context;
        public BlogPostController(IBlogPostService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var posts = await _context.Blogs
                .Include(p => p.ArtistProfile)
                .Include(p => p.Comments).ThenInclude(c => c.ArtistProfile)
                .Include(p => p.Likes).ThenInclude(l => l.ArtistProfile)
                .ToListAsync();
            return View(posts);
        }
        public async Task<IActionResult> MyPosts(int id)
        {
            var posts = await _context.Blogs
                .Include(p => p.ArtistProfile)
                .Include(p => p.Comments).ThenInclude(c => c.ArtistProfile)
                .Include(p => p.Likes).ThenInclude(l => l.ArtistProfile)
                .Where(p => p.ArtistProfile != null && p.ArtistProfile.ArtistProfileId == id)
                .ToListAsync();
            ViewBag.AristId = id;
            return View(posts);
        }
        public async Task<IActionResult> Details(int id)
        {
            var Bpost = await _service.GetByIdAsync(id);
            if (Bpost == null) return NotFound();
            return View(Bpost);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPost blogPost)
        {
            Console.WriteLine("Create action started");
            Console.WriteLine($"BlogPost data - Title: {blogPost.Title}, PostedBy: {blogPost.PostedBy}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is invalid");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                }
                return View(blogPost);
            }

            try
            {
                Console.WriteLine("Attempting to add blog post");
                await _service.AddAsync(blogPost);
                Console.WriteLine("Blog post added successfully");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating blog post: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                ModelState.AddModelError("", "An error occurred while creating the blog post.");
                return View(blogPost);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var Bpost = await _service.GetByIdAsync(id);
            if (Bpost == null) return NotFound();
            return View(Bpost);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int blogid, BlogPost blogPost, int UserId)
        {
            if (blogid != blogPost.BlogPostId || UserId != blogPost.PostedBy)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(blogPost);

            await _service.EditAsync(blogPost);
            return RedirectToAction("Details", new { blogid });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int UserId)
        {
            var Bpost = await _context.Blogs
                        .Include(p => p.ArtistProfile)
                        .ThenInclude(ap => ap.User)
                        .FirstOrDefaultAsync(p => p.BlogPostId == id);
            if (Bpost.PostedBy != UserId)
                return BadRequest();
            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }

    }
}
