using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;
using Test2.Database_Controller;
using Test2.Models;

namespace Test2.Pages.Viewers
{
    public class InfoModel : PageModel
    {
        //Dependency Injection
        private readonly SchoolContext _context;

        public InfoModel(SchoolContext context)
        {
            _context = context;
        }

        public Info? CurrentInfo { get; set; }

        public void OnGet()
        {
            CurrentInfo = _context.Infos.FirstOrDefault();

        }
    }
}
