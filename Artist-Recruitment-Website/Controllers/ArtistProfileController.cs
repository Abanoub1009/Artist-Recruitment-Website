using BL.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Linq;
using DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace Artist_Recruitment_Website.Controllers
{
    [Authorize]
    public class ArtistProfileController : Controller
    {
        private readonly IArtistProfileService _artistProfileService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _context;


        public ArtistProfileController(
            IArtistProfileService artistProfileService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IWebHostEnvironment webHostEnvironment,
            AppDbContext context)
        {
            _artistProfileService = artistProfileService;
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        public async Task<IActionResult> Index(string search)
        {
            var query = _context.ArtistProfiles
                .Include(p => p.User)
                .Where(p => p.User != null && p.User.FullName != null);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var lowered = search.ToLower();
                query = query.Where(p => p.User.FullName.ToLower().Contains(lowered));
            }

            var profiles = await query.ToListAsync();
            ViewBag.Search = search;
            return View(profiles);
        }

        public async Task<IActionResult> Details(int id)
        {
            var artistProfile = await _artistProfileService.GettByIdAsync(id);
            if (artistProfile == null)
            {
                return NotFound();
            }
            return View(artistProfile);
        }

        public async Task<IActionResult> Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("LoginSignup", "User");
            }
            var artistProfile = new ArtistProfile
            {
                UserId = Convert.ToInt32(userId)
            };
            return View(artistProfile);
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArtistProfile artistProfile, IFormFile ProfileImageFile, IFormFile CoverImageFile)
        {
            if (ProfileImageFile != null)
            {
                Console.WriteLine("profile image is not null");
                string profileImagePath = await SaveImage(ProfileImageFile, "profile");
                artistProfile.ProfileImage = profileImagePath;
                Console.WriteLine(artistProfile.ProfileImage);
            }

            // Handle cover image upload
            if (CoverImageFile != null)
            {
                Console.WriteLine("cover image is not null");
                string coverImagePath = await SaveImage(CoverImageFile, "cover");
                artistProfile.CoverImage = coverImagePath;
                Console.WriteLine(artistProfile.CoverImage);
            }
            TryValidateModel(artistProfile);
            ModelState.Remove(nameof(artistProfile.ProfileImage));
            ModelState.Remove(nameof(artistProfile.CoverImage));
            if (!ModelState.IsValid)
            {
                foreach (var entry in ModelState)
                {
                    foreach (var error in entry.Value.Errors)
                    {
                        Console.WriteLine($"Model error in '{entry.Key}': {error.ErrorMessage}");
                    }
                }
                return View(artistProfile);
            }


            // Verify the user is authenticated and matches the profile
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId) || artistProfile.UserId.ToString() != userId)
            {
                return RedirectToAction("LoginSignup", "User");
            }

            try
            { 
                await _artistProfileService.AddAsync(artistProfile);
                
                // Get the user and add ArtistProfileId claim
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    // Remove existing ArtistProfileId claim if it exists
                    var existingClaim = (await _userManager.GetClaimsAsync(user))
                        .FirstOrDefault(c => c.Type == "ArtistProfileId");
                    if (existingClaim != null)
                    {
                        await _userManager.RemoveClaimAsync(user, existingClaim);
                    }

                    // Add new ArtistProfileId claim
                    await _userManager.AddClaimAsync(user, new Claim("ArtistProfile", artistProfile.ArtistProfileId.ToString()));
                    
                    // Refresh the sign-in cookie to include the new claim
                    await _signInManager.RefreshSignInAsync(user);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while creating your profile. Please try again.");
                return View(artistProfile);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var artist = await _artistProfileService.GetByIdAsync(id);
            if (artist == null) return NotFound();

            // Get the current user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (Convert.ToInt32(userId) != artist.UserId)
            {
                return Forbid();
            }

            return View(artist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ArtistProfile artistProfile, IFormFile ProfileImageFile, IFormFile CoverImageFile)
        {
            if (id != artistProfile.ArtistProfileId)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(artistProfile);

            // Get the current user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (Convert.ToInt32(userId) != artistProfile.UserId)
            {
                return Forbid();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            // Handle profile image upload
            if (ProfileImageFile != null)
            {
                string profileImagePath = await SaveImage(ProfileImageFile, "profile");
                artistProfile.ProfileImage = profileImagePath;
            }

            // Handle cover image upload
            if (CoverImageFile != null)
            {
                string coverImagePath = await SaveImage(CoverImageFile, "cover");
                artistProfile.CoverImage = coverImagePath;
            }

            // Update user information
            user.FullName = artistProfile.User.FullName;
            user.Email = artistProfile.User.Email;
            user.PhoneNumber = artistProfile.User.PhoneNumber;

            var userUpdateResult = await _userManager.UpdateAsync(user);
            if (!userUpdateResult.Succeeded)
            {
                foreach (var error in userUpdateResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(artistProfile);
            }

            // Update artist profile
            await _artistProfileService.EditAsync(artistProfile);

            return RedirectToAction("Details", new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _artistProfileService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        private async Task<string> SaveImage(IFormFile file, string type)
        {
            if (file == null || file.Length == 0)
                return null;

            // Create unique filename
            string uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            
            // Create directory if it doesn't exist
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", type);
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Save file
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Return relative path for database storage
            return $"/uploads/{type}/{uniqueFileName}";
        }
    }
}
