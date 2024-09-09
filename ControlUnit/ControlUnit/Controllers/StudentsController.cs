using ControlUnit.Data;
using ControlUnit.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlUnit.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;

        public StudentsController
            (
            SchoolContext context
            )
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _context
                .Students.ToListAsync();

            return View(result);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(student);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes" + "try again");
            }
            return View(student);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
    }
}
