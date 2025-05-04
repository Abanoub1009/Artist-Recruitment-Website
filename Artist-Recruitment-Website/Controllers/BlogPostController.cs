using BL.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Artist_Recruitment_Website.Controllers
{
    public class BlogPostController : Controller
    {
        private readonly IBlogPostService _service;
        public BlogPostController(IBlogPostService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var Bposts = await _service.GetAllAsync();
            return View(Bposts);
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
            if (!ModelState.IsValid)
                return View(blogPost);

            await _service.AddAsync(blogPost);
            return RedirectToAction("Index");
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
            var Bpost = await _service.GetByIdAsync(id);
            if (Bpost.PostedBy != UserId)
                return BadRequest();
            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
