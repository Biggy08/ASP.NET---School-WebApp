
using Microsoft.EntityFrameworkCore;
using Test2.Database_Controller;
using Test2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --Add services to the container.
builder.Services.AddRazorPages(options =>
{
    //--Protecting all pages
    options.Conventions.AuthorizeFolder("/"); //lock all

    options.Conventions.AllowAnonymousToFolder("/Viewers"); //for viewers without credentials
    
    options.Conventions.AllowAnonymousToPage("/Index");  //exception for login page too
    


    );


 

//Configuring cookie settings for authentication
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Index";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    options.SlidingExpiration = true;
});


//Registering DbContext with the Dependency Injection Container
builder.Services.AddDbContext<SchoolContext>(options =>
    //--Adding Connection String for SQL Server Database
    //--can be done without modifying the appsettings.json file
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<SchoolContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // Create Admin role
    if (!await roleManager.RoleExistsAsync("Admin"))
        await roleManager.CreateAsync(new IdentityRole("Admin"));

    
     /* Creating Admin if doesn't exist (needed if 'admin' info gets deleted
    if (await userManager.FindByNameAsync("ram") == null)
    {
        var x = new User { UserName = "ram" };
        await userManager.CreateAsync(x, "ram123");
        await userManager.AddToRoleAsync(x, "Admin");
    }
    */
}



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles(); //Serve static files (css, js, images)
app.UseRouting();

//Middleware order
app.UseAuthentication(); //Authenticatino before authorization
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

// 1st page different by default
app.MapGet("/", context =>
{
    context.Response.Redirect("/Viewers/Viewer");
    return Task.CompletedTask;
});

app.Run();
