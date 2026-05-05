using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Test2.Database_Controller;
using Test2.Models;

namespace Test2.Pages
{

    public class NoticeModel : PageModel
    {
        [BindProperty]
        public Notice NewNotice { get; set; }

        //Dependency Injection
        private readonly SchoolContext _context;//adding DbContext

        public NoticeModel(SchoolContext context)
        {
            _context = context;
            NewNotice = new Notice();
        }

        public async Task<IActionResult> OnPostAddNoticeAsync()   //asp-page handler = AddNotice
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Notices.Add(NewNotice);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Notice");
        }

        public void OnGet()
        {
        }
    }
}
