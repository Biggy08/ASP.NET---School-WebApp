using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Test2.Database_Controller;
using Test2.Models; 

namespace Test2.Pages
{
    public class FacultyDataModel : PageModel
    {
        // Dependency Injection
        private readonly SchoolContext _context;

        public FacultyDataModel(SchoolContext context)
        {
            _context = context;
        }
        /*-----------------------------------------------------------*/

        // Properties

        public List<Faculty> FacultyInfo { get; set; } = new List<Faculty>(); // initialized to empty to prevent null

        public void OnGet()
        {
            FacultyInfo = _context.Faculty.ToList();  //Entire details for faculty card

            if (!FacultyInfo.Any())
            {
                FacultyInfo = new List<Faculty>();    
            }
        }

        // Delete Handler
        public IActionResult OnPostDelete(int id)
        {
            var faculty = _context.Faculty.Find(id); // finds only the faculty with matching id

            if (faculty != null)
            {
                _context.Faculty.Remove(faculty); // removes only that one record
                _context.SaveChanges();
            }

            return RedirectToPage(); // reloads the page after deletion
        }
    }
}