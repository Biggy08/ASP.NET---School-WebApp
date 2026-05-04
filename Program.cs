using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Test2.Database_Controller;

var builder = WebApplication.CreateBuilder(args);

// --Add services to the container.
builder.Services.AddRazorPages(options =>
{
    //--Protecting all pages
    options.Conventions.AuthorizeFolder("/"); //lock all
    options.Conventions.AllowAnonymousToPage("/Index"); //exception

}
    );

//--Adding Authentication Service - cookie config
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Index"; //--Redirect to Index page for login
        options.ExpireTimeSpan = TimeSpan.FromMinutes(10); // --cookie expire time (idle timeout)
        options.SlidingExpiration = true; //reset timer on activty 
    });

//Registering DbContext with the Dependency Injection Container
builder.Services.AddDbContext<SchoolContext>(options =>
    //--Adding Connection String for SQL Server Database
    //--can be done without modifying the appsettings.json file
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

//Middleware order
app.UseAuthentication(); //Authenticatino before authorization
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
