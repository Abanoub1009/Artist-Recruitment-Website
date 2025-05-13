using BL.Services;
using BL.Services.Interface;
using DAL.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Security.Claims;

namespace Artist_Recruitment_Website.Controllers
{
    public class JobPostController : Controller
    {
        private readonly IJobPostService _jobPostService;
        public JobPostController(IJobPostService jobPostService)
        {
            _jobPostService = jobPostService;
        }
        public async Task<IActionResult> Index()
        {
            var jobPosts = await _jobPostService.GetAllJobPostsAsync();
            return View(jobPosts);
        }
        public async Task<IActionResult> Details(int id)
        {
            var jobPost = await _jobPostService.GetJobPostByIdAsync(id);
            if (jobPost == null) return NotFound();

            return View(jobPost);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobPost jobPost)
        {
            if (!ModelState.IsValid)
                return View(jobPost);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            jobPost.CreatedAt = DateTime.Now;

            await _jobPostService.AddJobPostAsync(jobPost);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var jobPost = await _jobPostService.GetJobPostByIdAsync(id);
            if (jobPost == null) return NotFound();

            return View(jobPost);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, JobPost jobPost)
        {
            if (id != jobPost.JobPostId)
                return NotFound();

            if (!ModelState.IsValid)
                return View(jobPost);

            await _jobPostService.UpdateJobPostAsync(jobPost);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var jobPost = await _jobPostService.GetJobPostByIdAsync(id);
            if (jobPost == null) return NotFound();

            return View(jobPost);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _jobPostService.DeleteJobPostByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}