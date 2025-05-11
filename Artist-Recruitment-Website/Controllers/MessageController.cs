using Microsoft.AspNetCore.Mvc;

namespace Artist_Recruitment_Website.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
