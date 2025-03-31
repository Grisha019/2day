using System.ComponentModel.DataAnnotations;

namespace _2day.Model
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Один преподаватель может вести несколько курсов
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
