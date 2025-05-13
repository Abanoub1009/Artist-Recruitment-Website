using BL.Services.Interface;
using DAL.Repository;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Artist_Recruitment_Website.Controllers
{
    public class PortfolioItemController : Controller
    {
        private readonly IPortfolioItemService _portfolioItemService;
        public PortfolioItemController(IPortfolioItemService portfolioItemService)
        {
            _portfolioItemService = portfolioItemService;
        }
        public async Task<IActionResult> Index()
        {
            var items = await _portfolioItemService.GetAllAsync();
            return View(items);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _portfolioItemService.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            return View(item);
        }

        // GET: PortfolioItem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PortfolioItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PortfolioItem item)
        {
            if (ModelState.IsValid)
            {
                await _portfolioItemService.AddAsync(item);
                return RedirectToAction(nameof(Index));
            }
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
        public async Task<IActionResult> Edit(int id, PortfolioItem item)
        {
            if (id != item.PortfolioItemId)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _portfolioItemService.UpdateStatusAsync(item);
                return RedirectToAction(nameof(Index));
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
            await _portfolioItemService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }

}