using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Test2.Database_Controller;

namespace Test2.Pages
{
    [Authorize (Roles = "Admin")]
    public class AdminModel : PageModel
    {
        public int StudentCount { get; set; }

        public void OnGet()
        {
            
        }
    }
}
