using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Test2.Database_Controller;
using Test2.Models;

namespace Test2.Pages
{
    public class HomeModel : PageModel
    {

        public int StudentCount { get; set; }

        public int FacultyCount { get; set; }

        public List<Notice> NoticeMsg { get; set; } = new();


        //Dependency Injection
        private readonly SchoolContext _context;//adding DbContext 
        public HomeModel(SchoolContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            //Going into database through _context and counting
            StudentCount = _context.Students.Count();

            FacultyCount = _context.Faculty.Count();

            NoticeMsg = _context.Notices.ToList();

        }
    }
}
