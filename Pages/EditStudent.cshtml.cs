using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Test2.Database_Controller;
using Test2.Models;

namespace Test2.Pages
{
    public class EditStudentModel : PageModel
    {

        // Dependency Injection 
        private readonly SchoolContext _context;

        public EditStudentModel(SchoolContext context) => _context = context;

        [BindProperty]
        public Student Student { get; set; } = new Student();

        public SelectList Courses { get; set; } = new SelectList(Enumerable.Empty<object>());

        // GET: load existing student data into form
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Student = await _context.Students.FindAsync(id) ?? new Student();

            if (Student == null) return NotFound();

            Courses = new SelectList(_context.Courses, "Id", "Title");

            return Page();
        }

        // POST: save changes to database
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            _context.Attach(Student).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToPage("/StudentData");
        }
    }
}