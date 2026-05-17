using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Test2.Database_Controller;

namespace Test2.Pages.Viewers
{
    public class FamilyModel : PageModel
    {
        //Dependency Injection (to access the database)
        private readonly SchoolContext _context;

        public FamilyModel (SchoolContext context)
        {
            _context = context;
        }

        //Properties to Stored data from Database

        public int StudentCount { get; set; }

        public void OnGet()
        {
            StudentCount = _context.Students.Count();
        }
    }
}
