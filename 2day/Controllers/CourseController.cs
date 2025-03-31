using _2day.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _2day.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly SchoolContext _context;
        public CourseController(SchoolContext context)
        {
            _context = context;
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return await _context.Courses.Include(c => c.Teacher).Include(c => c.Students).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Teacher)
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (course == null) return NotFound();
            return course;
        }

        [HttpPost]
        public async Task<ActionResult<Course>> CreateCourse([FromBody] Course course)
        {
            if (course == null)
            {
                return BadRequest(new { message = "Данные курса отсутствуют." });
            }

            if (string.IsNullOrEmpty(course.Title))
            {
                return BadRequest(new { message = "Название курса обязательно." });
            }

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, Course course)
        {
            if (id != course.Id) return BadRequest();
            _context.Entry(course).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Добавить студента на курс
        [HttpPost("{courseId}/students/{studentId}")]
        public async Task<IActionResult> AddStudentToCourse(int courseId, int studentId)
        {
            var course = await _context.Courses.Include(c => c.Students).FirstOrDefaultAsync(c => c.Id == courseId);
            var student = await _context.Students.FindAsync(studentId);

            if (course == null || student == null) return NotFound();

            if (!course.Students.Contains(student))
            {
                course.Students.Add(student);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }

        // Назначить преподавателя курсу
        [HttpPost("{courseId}/teacher/{teacherId}")]
        public async Task<IActionResult> AssignTeacherToCourse(int courseId, int teacherId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            var teacher = await _context.Teachers.FindAsync(teacherId);

            if (course == null || teacher == null) return NotFound();

            course.TeacherId = teacherId;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
