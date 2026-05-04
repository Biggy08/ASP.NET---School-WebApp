using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Test2.Database_Controller;
using Test2.Models;

namespace Test2.Pages
{
    public class StudentDataModel : PageModel
    {

        public int StudentCount { get; set; }

        //For bar chart
        public int ComputerCount { get; set; }
        public int CivilCount { get; set; }
        public int ElectricalCount { get; set; }

        //For Result Status
        public int PassCount { get; set; }
        public int FailCount { get; set; }

        // For Top 5 students
        public List<Student> TopStudents { get; set; } = [];
        //For all students
        public List<Student> AllStudents { get; set; } = [];

        [BindProperty]
        public Student NewStudent { get; set; } = new Student();

        public SelectList Courses { get; set; } = new SelectList(Enumerable.Empty<object>()); //making drop down empty (initially)
        //Select list : For drop down menus

        //Dependency Injection
        private readonly SchoolContext _context;//adding DbContext 
        public StudentDataModel(SchoolContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            //Going into database through _context and counting
            StudentCount = _context.Students.Count();

            //Result Status
            PassCount = _context.Students.Count(s=> s.Result == true); 
            FailCount = _context.Students.Count(s => s.Result == false);



            //For bargraph 1:
            ComputerCount = _context.Students
             .Count(s => s.Course != null && s.Course.Title == "Computer");

            CivilCount = _context.Students
                .Count(s => s.Course != null && s.Course.Title == "Civil");

            ElectricalCount = _context.Students
                .Count(s => s.Course != null && s.Course.Title == "Electrical");


            //For Top 5 students based on Score
            // Add this property

            TopStudents = _context.Students
                .Include(s=>s.Course)   //Adding course of each student
                .OrderByDescending(s => s.Score)
                .Take(5)
                .ToList();

            AllStudents = _context.Students
                .Include(s => s.Course)
                .OrderBy(s => s.SName)
                .ToList();

            Courses = new SelectList(_context.Courses, "Id", "Title");
        }

        //Adding Students:
        public async Task<IActionResult> OnPostAddStudentAsync()   //asp-page handler = AddStudent
        {
            if (!ModelState.IsValid) return Page(); //field validation

            _context.Students.Add(NewStudent); // New object -> EF Core
            await _context.SaveChangesAsync(); // insert into database

            return RedirectToPage();
        }


        //Deleting Students
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }


    }
}
