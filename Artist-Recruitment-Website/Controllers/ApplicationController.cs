using BL.Services.Interface;
using DAL.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models.Models;

namespace Artist_Recruitment_Website.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly IApplicationService _service;

        public ApplicationController(IApplicationService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var apps = await _service.GetAllAsync();
            return View(apps);
        }

        public async Task<IActionResult> Details(int id)
        {
            var app = await _service.GetByIdAsync(id);
            if (app == null) return NotFound();
            return View(app);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Application app)
        {
            if (!ModelState.IsValid)
                return View(app);

            await _service.AddAsync(app);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            await _service.UpdateStatusAsync(id, status);
            return RedirectToAction("Details", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
