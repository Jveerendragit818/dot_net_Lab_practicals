
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using SmartCampusServicePortal.Data;
    using SmartCampusServicePortal.Models;

    namespace SmartCampusServicePortal.Controllers
    {
        public class StudentsController : Controller
        {
            private readonly ApplicationDbContext _context;

            public StudentsController(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<IActionResult> Index()
            {
                var students = await _context.Students
                    .AsNoTracking()
                    .ToListAsync();

                return View(students);
            }

            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(Student student)
            {
                if (!ModelState.IsValid)
                    return View(student);

                _context.Students.Add(student);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Student added successfully.";

                return RedirectToAction(nameof(Index));
            }

            public async Task<IActionResult> Edit(int id)
            {
                var student = await _context.Students.FindAsync(id);

                if (student == null)
                    return NotFound();

                return View(student);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(
                int id, Student student)
            {
                if (id != student.Id)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return View(student);

                var existing = await _context.Students.FindAsync(id);

                if (existing == null)
                    return NotFound();

                existing.Name = student.Name;
                existing.Email = student.Email;
                existing.Phone = student.Phone;
                existing.Department = student.Department;
                existing.Course = student.Course;

                await _context.SaveChangesAsync();

                TempData["Success"] = "Student updated successfully.";

                return RedirectToAction(nameof(Index));
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
                return NotFound();

            var requests = await _context.ServiceRequests
                .Where(r => r.StudentId == id)
                .ToListAsync();

            _context.ServiceRequests.RemoveRange(requests);

            _context.Students.Remove(student);

            await _context.SaveChangesAsync();

            TempData["Success"] = "Student deleted successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
    }
