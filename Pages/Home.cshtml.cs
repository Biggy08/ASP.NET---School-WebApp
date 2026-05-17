using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Test2.Database_Controller;
using Test2.Models;

namespace Test2.Pages
{
    public class HomeModel : PageModel
    {

        //Properties to store data that will be displayed later
        public int StudentCount { get; set; }

        public int FacultyCount { get; set; }

        public List<Notice> NoticeMsg { get; set; } = new();


        //Dependency Injection
        private readonly SchoolContext _context;//adding DbContext 
        private readonly IWebHostEnvironment _env; 
        public HomeModel(SchoolContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public void OnGet()
        {
            //Going into database through _context and counting
            StudentCount = _context.Students.Count();

            FacultyCount = _context.Faculty.Count();

            NoticeMsg = _context.Notices.ToList();

        }

        public async Task<IActionResult> OnPostUploadImageAsync(IFormFile image, string filename)
        {
            if (image == null || string.IsNullOrEmpty(filename))
                return BadRequest();

            var safeName = Path.GetFileName(filename);
            var path = Path.Combine(_env.WebRootPath, "images", safeName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return new OkResult();
        }


    }
}
