using BL.Services;
using BL.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Artist_Recruitment_Website.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var items = await _reviewService.GetAllReviewsAsync();
            var res = items.Where(op => op.ArtistProfileId == id);
            ViewBag.ArtistProfileId = id;
            return View(res);
        }

        public async Task<IActionResult> Details(int id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            if (review == null)
                return NotFound();

            return View(review);
        }

        public IActionResult Create(int id)
        {
            var review = new Review();
            review.ArtistProfileId = id; // or set any other required properties
            ViewBag.ArtistProfileId = id;
            return View(review);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Review review)
        {
            if (!ModelState.IsValid)
                return View(review);

            await _reviewService.AddReviewAsync(review);
            return RedirectToAction(nameof(Index), new { id = review.ArtistProfileId });
        }
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            if (review == null)
                return NotFound();

            return View(review);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Load the item first
            var item = await _reviewService.GetReviewByIdAsync(id);
            if (item == null)
                return NotFound();

            await _reviewService.DeleteReviewAsync(id);
            // Now you can use item.ArtistProfileId for the redirect
            return RedirectToAction(nameof(Index), new { id = item.ArtistProfileId });
        }
    }
}