using Microsoft.AspNetCore.Mvc;
using Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Data;

namespace YourProject.Controllers
{
    public class NotificationController : Controller
    {
        private readonly AppDbContext _context;

        public NotificationController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int aritsitProfileId)
        {
            var notifications = await _context.Notifications
                .Where(n => n.ArtistProfileId == aritsitProfileId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

            return View(notifications);
        }

        // عرض تفاصيل إشعار
        public async Task<IActionResult> Details(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null) return NotFound();
            return View(notification);
        }

        // عرض فورم إنشاء إشعار (للأدمن)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // تنفيذ إنشاء الإشعار
        [HttpPost]
        public async Task<IActionResult> Create(int ArtistProfileId, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                ModelState.AddModelError("Content", "المحتوى مطلوب.");
                return View();
            }

            var notification = new Notification
            {
                ArtistProfileId = ArtistProfileId,
                Content = content,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index","Home");
        }

        // تعليم إشعار كمقروء
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null) return NotFound();

            notification.IsRead = true;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { userId = notification.ArtistProfileId });
        }

        // حذف إشعار
        public async Task<IActionResult> Delete(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null) return NotFound();

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
