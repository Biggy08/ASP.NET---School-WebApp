using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

//Student Table
namespace Test2.Models
{

    //Table Definition for Students in SQL Database
    public class Student
    {
        //Columns
        [Key]
        public int Id { get; set; }
        public string? SName { get; set; }

        public string? Address { get; set; }

        public string? Contact { get; set; }

        public Boolean Result { get; set; }

        public int Score { get; set; }

        // Foreign key property
        public int? CourseId { get; set; }

        // Navigation property to Course
        public Course? Course { get; set; }
    }
}
