using BL.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Artist_Recruitment_Website.Controllers
{
    public class ArtistProfileController : Controller
    {
        private readonly IArtistProfileService _service;
        public ArtistProfileController(IArtistProfileService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var artists = await _service.GetAllAsync();
            return View(artists);
        }
        public async Task<IActionResult> Details(int id)
        {
            var artist = await _service.GetByIdAsync(id);
            if (artist == null) return NotFound();
            return View(artist);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArtistProfile artistProfile)
        {
            if (!ModelState.IsValid)
                return View(artistProfile);

            await _service.AddAsync(artistProfile);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var artist = await _service.GetByIdAsync(id);
            if (artist == null) return NotFound();
            return View(artist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, ArtistProfile artistProfile)
        {
            if (id != artistProfile.ArtistProfileId)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(artistProfile);

            await _service.EditAsync(artistProfile);
            return RedirectToAction("Details", new { id });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }

    }
}
