using BL.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Artist_Recruitment_Website.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var users = await _service.GetAllAsync();
            return View(users);
        }
        public async Task<IActionResult> Details(int id)
        {
            var user = await _service.GetByIdAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (!ModelState.IsValid)
                return View(user);

            await _service.AddAsync(user);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _service.GetByIdAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, User user)
        {
            if (id != Convert.ToInt32(user.Id))
                return BadRequest();

            if (!ModelState.IsValid)
                return View(user);

            await _service.EditAsync(user);
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
