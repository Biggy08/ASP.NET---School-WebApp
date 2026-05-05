using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;


namespace Test2.Models
{
    public class Faculty
    {

        public int Id { get; set; }

        public string? FName { get; set; }

        public string? Address { get; set; }

        public string? Contact { get; set; }

        //Foreign key property
        public int? CourseId { get; set; }

        //Navigation Property
        public Course? Course { get; set; }
    }
}
