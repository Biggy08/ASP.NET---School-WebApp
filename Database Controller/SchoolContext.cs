using Microsoft.EntityFrameworkCore;
using Test2.Models;

namespace Test2.Database_Controller
{
    public class SchoolContext: DbContext
    {
        // Class Student set as a DbSet to be used in the database context for mulitple students 
        public DbSet<Student> Students { get; set; } //representing Student table in database
        public DbSet<User> Users { get; set; } //User (Entity) -> Uesrs (DbSet) table   

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
