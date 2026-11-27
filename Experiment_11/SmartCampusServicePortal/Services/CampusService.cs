
using Microsoft.EntityFrameworkCore;
using SmartCampusServicePortal.Data;

namespace SmartCampusServicePortal.Services
{
    public class CampusService
    {
        private readonly ApplicationDbContext _context;

        public CampusService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<object> GetDashboardData()
        {
            return new
            {
                TotalStudents = await _context.Students.CountAsync(),

                TotalRequests = await _context.ServiceRequests.CountAsync(),

                PendingRequests = await _context.ServiceRequests
                    .CountAsync(x => x.Status == "Pending"),

                TotalNotices = await _context.Notices.CountAsync(),

                TotalEvents = await _context.CampusEvents.CountAsync()
            };
        }
    }
}
