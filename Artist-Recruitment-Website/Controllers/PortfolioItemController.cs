using BL.Services.Interface;
using DAL.Data;
using DAL.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Artist_Recruitment_Website.Controllers
{
    [Authorize]
    public class PortfolioItemController : Controller
    {
        private readonly IPortfolioItemService _portfolioItemService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _context;
        public PortfolioItemController(IPortfolioItemService portfolioItemService, IWebHostEnvironment webHostEnvironment, AppDbContext context)
        {
            _portfolioItemService = portfolioItemService;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }
        public async Task<IActionResult> Index(int id)
        {
            var items = await _portfolioItemService.GetAllAsync();
            var res = items.Where(op => op.ArtistProfileId == id);
            ViewBag.ArtistProfileId = id;
            return View(res);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _context.PortfolioItems
                        .Include(p => p.ArtistProfile)
                        .ThenInclude(ap => ap.User)
                        .FirstOrDefaultAsync(p => p.PortfolioItemId == id);
            if (item == null)
                return NotFound();
            Console.WriteLine(item.ArtistProfile.WeightInKg);
            return View(item);
        }

        // GET: PortfolioItem/Create
        public IActionResult Create(int id)
        {
            ViewBag.ArtistProfileId = id;
            return View();
        }

        // POST: PortfolioItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PortfolioItem item, IFormFile Media)
        {
            ModelState.Remove(nameof(item.MediaUrl));
            if (ModelState.IsValid)
            {
                try
                {
                    if (Media != null && Media.Length > 0)
                    {
                        // Validate file type
                        var allowedTypes = new[] { ".jpg", ".jpeg", ".png" };
                        var extension = Path.GetExtension(Media.FileName).ToLowerInvariant();
                        
                        if (!allowedTypes.Contains(extension))
                        {
                            ModelState.AddModelError("MediaUrl", "Please upload a valid image file (JPG, JPEG, or PNG).");
                            return View(item);
                        }

                        // Validate file size (5MB max)
                        if (Media.Length > 5 * 1024 * 1024)
                        {
                            ModelState.AddModelError("MediaUrl", "Image size must be less than 5MB.");
                            return View(item);
                        }

                        // Create unique filename
                        var fileName = Path.GetRandomFileName() + extension;
                        var filePath = Path.Combine("wwwroot", "uploads", "portfolio", fileName);

                        // Ensure directory exists
                        Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                        // Save file
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await Media.CopyToAsync(stream);
                        }

                        // Set the MediaUrl
                        item.MediaUrl = "/uploads/portfolio/" + fileName;
                        Console.WriteLine(item.MediaUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("MediaUrl", "Please select an image file.");
                        return View(item);
                    }

                    await _portfolioItemService.AddAsync(item);
                    return RedirectToAction(nameof(Index), new { id = item.ArtistProfileId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while creating the portfolio item. Please try again.");
                    Console.WriteLine($"Error creating portfolio item: {ex.Message}");
                }
            }

            // Display all validation errors
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    Console.WriteLine($"Field: {state.Key}, Error: {error.ErrorMessage}");
                }
            }
            
            ViewBag.ArtistProfileId = item.ArtistProfileId;
            return View(item);
        }

        // GET: PortfolioItem/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _portfolioItemService.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            return View(item);
        }

        // POST: PortfolioItem/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PortfolioItem item, IFormFile? Media)
        {
            if (ModelState.IsValid)
                Console.WriteLine(item.MediaUrl);
            if (Media != null && Media.Length > 0)
            {
                // Validate file type
                var allowedTypes = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(Media.FileName).ToLowerInvariant();

                if (!allowedTypes.Contains(extension))
                {
                    ModelState.AddModelError("MediaUrl", "Please upload a valid image file (JPG, JPEG, or PNG).");
                    return View(item);
                }

                // Validate file size (5MB max)
                if (Media.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("MediaUrl", "Image size must be less than 5MB.");
                    return View(item);
                }

                // Create unique filename
                var fileName = Path.GetRandomFileName() + extension;
                var filePath = Path.Combine("wwwroot", "uploads", "portfolio", fileName);

                // Ensure directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                // Save file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Media.CopyToAsync(stream);
                }

                // Set the MediaUrl
                item.MediaUrl = "/uploads/portfolio/" + fileName;
                ModelState.Remove(nameof(item.MediaUrl));
                Console.WriteLine(item.MediaUrl);
            }
            if (id != item.PortfolioItemId)
                return BadRequest();
            if (ModelState.IsValid)
            {
                await _portfolioItemService.UpdateStatusAsync(item);
                return RedirectToAction(nameof(Index), new { id = item.ArtistProfileId });
            }
            return View(item);
        }

        // GET: PortfolioItem/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _portfolioItemService.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            return View(item);
        }

        // POST: PortfolioItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Load the item first
            var item = await _portfolioItemService.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            await _portfolioItemService.DeleteAsync(id);
            // Now you can use item.ArtistProfileId for the redirect
            return RedirectToAction(nameof(Index), new { id = item.ArtistProfileId });
        }

    }

}