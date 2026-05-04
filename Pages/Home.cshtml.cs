using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Test2.Database_Controller;

namespace Test2.Pages
{
    public class HomeModel : PageModel
    {

        public int StudentCount { get; set; }


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

        }
    }
}
