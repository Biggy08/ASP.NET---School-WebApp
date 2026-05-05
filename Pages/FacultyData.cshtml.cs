using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Test2.Database_Controller;

namespace Test2.Pages
{
    public class FacultyDataModel : PageModel
    {

        //Dependency Injection
        private readonly SchoolContext _context;//adding DbContext

        public FacultyDataModel(SchoolContext context)
        {
            _context = context;
        }
        /*-----------------------------------------------------------*/

        //Properties 
        public int FacultyCount { get; set; }

        public void OnGet()
        {
            FacultyCount = _context.Faculty.Count();

        }
    }
}
