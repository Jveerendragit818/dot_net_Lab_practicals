using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCampusServicePortal.Data;

namespace SmartCampusServicePortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var stats = new
            {
                TotalStudents = await _context.Students.CountAsync(),

                TotalRequests = await _context.ServiceRequests.CountAsync(),

                PendingRequests = await _context.ServiceRequests
                    .CountAsync(r => r.Status == "Pending"),

                TotalNotices = await _context.Notices.CountAsync(),

                TotalEvents = await _context.CampusEvents.CountAsync()
            };

            ViewBag.Stats = stats;

            return View();
        }
    }
}