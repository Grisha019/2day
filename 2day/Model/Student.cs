using System.ComponentModel.DataAnnotations;

namespace _2day.Model
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Студент может записаться на несколько курсов (Many-to-Many)
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
