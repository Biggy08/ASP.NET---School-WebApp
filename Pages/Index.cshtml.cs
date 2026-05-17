using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Test2.Database_Controller;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Test2.Models;
using Microsoft.EntityFrameworkCore;

namespace Test2.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string? Name { get; set; }

        [BindProperty]
        public string? Password { get; set; }

        // Replace _context injection with Identity managers
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public IndexModel(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //  Basic input validation
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Password))
                return Page();

            //  Identity handles password verification internally
            //checking UserName and Password  with AspNetUsers table
            var result = await _signInManager.PasswordSignInAsync(
                userName: Name,
                password: Password,
                isPersistent: false,
                lockoutOnFailure: true  // locks after repeated failed attempts (Brute force protection)
            );

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(Name);  //finding user
                var roles = await _userManager.GetRolesAsync(user!);  //checking their role AspNetUserRoles + AspNetRoles


                if (roles.Contains("Admin"))
                    return RedirectToPage("/Admin");   //only admins directed to admin page 
                else
                    return RedirectToPage("#");
            }

            // Locked out check
            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Account locked. Try again later.");
                return Page();
            }

            // Wrong credentials
            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return Page();
        }

        public void OnGet() { }
    }
}