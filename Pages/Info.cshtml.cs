using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Test2.Database_Controller;
using Test2.Models;

namespace Test2.Pages
{
    public class InfoModel : PageModel
    {

        //Dependency Injection
        private readonly SchoolContext _context;

        public InfoModel(SchoolContext context)
        {
            _context = context;
        }

        //property
        public Info? CurrentInfo { get; set; }

        [BindProperty]
        public string? NewText { get; set; }
        public void OnGet()
        {
            CurrentInfo = _context.Infos.FirstOrDefault();
        }

        public IActionResult OnPost()
        {
            var info = _context.Infos.FirstOrDefault();

            if (info == null)
            {
                // if no info record, create
                _context.Infos.Add(new Info { Text = NewText });
            }
            else
            {
                // Update the existing record
                info.Text = NewText;
                _context.Infos.Update(info);
            }

            _context.SaveChanges();

            CurrentInfo = _context.Infos.FirstOrDefault();
            return Page();
        }
    }

}
