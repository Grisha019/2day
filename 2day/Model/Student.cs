using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2day.Model
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string Name { get; set; } = string.Empty;

        // Студент может записаться на несколько курсов (Many-to-Many)
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
