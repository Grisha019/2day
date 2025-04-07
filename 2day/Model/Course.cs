using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2day.Model
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;

        // Один курс принадлежит одному преподавателю
        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }

        // Один курс может включать нескольких студентов (Many-to-Many)
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
