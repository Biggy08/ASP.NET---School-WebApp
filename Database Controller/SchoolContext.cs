using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Test2.Models;

namespace Test2.Database_Controller
{
    //Inheriting 'IdentityDbContext' to integrate ASP.NET core Identity features (User management, authentication, authorization)
    //It creates table liek AspNetUsers, AspNetRoles, AspNetUserRoles, etc. for Identity management
    public class SchoolContext : IdentityDbContext<User>
    {
        // Class Student set as a DbSet to be used in the database context for mulitple students 
        public DbSet<Student> Students { get; set; } //representing Student table in database

        public DbSet<Faculty> Faculty { get; set; }

        public DbSet<Notice> Notices { get; set; }

        public DbSet<Info> Infos { get; set; }

        public DbSet<Course> Courses { get; set; } //Course (Entity) -> Courses (DbSet) table
        //Student : C# model (entity)
        //Stduents : DbSet property name   
        //Different Names  to avoid confusion between MODEL and DBSET


        //Constructor = runs autoatically when creating object of SchoolContext class
        public SchoolContext(DbContextOptions options): base(options)
        {
            //DbContextOptions -> Option -> Base class  
            //options : contains Db cofiguration  ( Connection string , provider, ..etc)
        }
    }
}
