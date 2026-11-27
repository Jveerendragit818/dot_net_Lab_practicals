using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCampusServicePortal.Data;
using SmartCampusServicePortal.Models;

namespace SmartCampusServicePortal.Controllers.API
{
    [ApiController]
    [Route("api/campus")]
    public class CampusApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CampusApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("students")]
        public async Task<IActionResult> GetStudents()
        {
            return Ok(await _context.Students.ToListAsync());
        }

        [HttpGet("requests")]
        public async Task<IActionResult> GetRequests()
        {
            var data = await _context.ServiceRequests
                .Select(x => new
                {
                    x.Id,
                    x.StudentId,
                    x.Title,
                    x.Description,
                    x.Status,
                    x.CreatedAt
                })
                .ToListAsync();

            return Ok(data);
        }

        [HttpPost("students")]
        public async Task<IActionResult> AddStudent(Student student)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return Ok(student);
        }

        [HttpPost("requests")]
        public async Task<IActionResult> AddRequest(ServiceRequest request)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            if (request.StudentId.HasValue &&
                !await _context.Students.AnyAsync(
                    x => x.Id == request.StudentId.Value))
            {
                return BadRequest("Invalid StudentId");
            }

            request.Id = 0;
            request.Status = "Pending";
            request.CreatedAt = DateTime.Now;

            _context.ServiceRequests.Add(request);
            await _context.SaveChangesAsync();

            return Ok(request);
        }

        [HttpDelete("students/{id:int}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
                return NotFound();

            if (await _context.ServiceRequests.AnyAsync(
                x => x.StudentId == id))
            {
                return Conflict(
                    "Cannot delete student while service requests reference this student.");
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("requests/{id:int}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var request = await _context.ServiceRequests.FindAsync(id);

            if (request == null)
                return NotFound();

            _context.ServiceRequests.Remove(request);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}