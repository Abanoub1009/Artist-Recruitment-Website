using BL.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Artist_Recruitment_Website.Controllers
{
    public class RecruiterProfileController : Controller
    {
        private readonly IRecruiterProfileService _service;
        public RecruiterProfileController(IRecruiterProfileService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var recruiters = await _service.GetAllAsync();
            return View(recruiters);
        }
        public async Task<IActionResult> Details(int id)
        {
            var recruiter = await _service.GetByIdAsync(id);
            if (recruiter == null) return NotFound();
            return View(recruiter);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RecruiterProfile recruiterProfile)
        {
            if (!ModelState.IsValid)
                return View(recruiterProfile);

            await _service.AddAsync(recruiterProfile);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var recruiter = await _service.GetByIdAsync(id);
            if (recruiter == null) return NotFound();
            return View(recruiter);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, RecruiterProfile recruiterProfile)
        {
            if (id != recruiterProfile.RecruiterProfileId)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(recruiterProfile);

            await _service.EditAsync(recruiterProfile);
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
