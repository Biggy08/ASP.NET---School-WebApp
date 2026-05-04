using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Test2.Database_Controller;
using System.Security.Claims;


using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Test2.Pages
{
    public class IndexModel : PageModel
    {
        //1. Model Binding  (Form Data -> C# properties)
        // Matches name with frontend form and maps it to these properties
        [BindProperty]
        public string? Name { get; set; }

        [BindProperty]
        public string? Password{ get; set; }


        //2. Dependency Injection 
        // To get instance of Db Context from Container
        // Assign it to the private field _context  
        private readonly SchoolContext _context;//adding DbContext 
        public IndexModel(SchoolContext context)
        {
            _context = context;     
        }
        //constructor of IndexModel taking  context as an input  
        //to use the context for db operations 


        //Form Submission handler
        //3. Asynchronous Programming (async/await)
        // runs when post request is  sent from frontend 
        public async Task<IActionResult> OnPostAsync()
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Name == Name && u.Password == Password);

            //Wrong Credentials , stay on the same page
            if (user == null)
                return Page();

            // Creating identity cookie
            //Letting ASP.NET know user has logged in

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name ?? string.Empty)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);


        //sing in-> auth cookie creation
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return RedirectToPage("/Admin");
        }
        public void OnGet()
        {

        }
    }
}
