using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2day.Model
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }
        public string Name { get; set; } = string.Empty;

        // Один преподаватель может вести несколько курсов
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
