using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCampusServicePortal.Data;
using SmartCampusServicePortal.Models;

namespace SmartCampusServicePortal.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.CampusEvents
                .OrderBy(x => x.EventDate)
                .ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CampusEvent campusEvent)
        {
            if (!ModelState.IsValid)
                return View(campusEvent);

            _context.CampusEvents.Add(campusEvent);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.CampusEvents.FindAsync(id);

            if (item == null)
                return NotFound();

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CampusEvent campusEvent)
        {
            if (id != campusEvent.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(campusEvent);

            var existing = await _context.CampusEvents.FindAsync(id);

            if (existing == null)
                return NotFound();

            existing.Title = campusEvent.Title;
            existing.Description = campusEvent.Description;
            existing.EventDate = campusEvent.EventDate;
            existing.Location = campusEvent.Location;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.CampusEvents.FindAsync(id);

            if (item == null)
                return NotFound();

            _context.CampusEvents.Remove(item);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}