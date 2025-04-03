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

        // GET: api/courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            // При необходимости можно Include связанные сущности
            return await _context.Courses
                .Include(c => c.Teacher)
                .Include(c => c.Students)
                .ToListAsync();
        }

        // GET: api/courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Teacher)
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (course == null)
                return NotFound();
            return course;
        }

        // POST: api/courses
        [HttpPost]
        public async Task<ActionResult<Course>> CreateCourse([FromBody] Course course)
        {
            // Если в теле запроса не указан преподаватель, он может быть назначен отдельно
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
        }

        // PUT: api/courses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] Course course)
        {
            if (id != course.Id)
                return BadRequest();

            _context.Entry(course).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Courses.Any(c => c.Id == id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }

        // DELETE: api/courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return NotFound();

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/courses/5/enroll?studentId=10
        [HttpPost("{id}/enroll")]
        public async Task<IActionResult> EnrollStudent(int id, [FromQuery] int studentId)
        {
            var course = await _context.Courses
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (course == null)
                return NotFound("Курс не найден");

            var student = await _context.Students.FindAsync(studentId);
            if (student == null)
                return NotFound("Студент не найден");

            // Проверка, не записан ли уже студент на курс
            if (!course.Students.Any(s => s.Id == studentId))
            {
                course.Students.Add(student);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }

        // POST: api/courses/5/assign-teacher?teacherId=3
        [HttpPost("{id}/assign-teacher")]
        public async Task<IActionResult> AssignTeacher(int id, [FromQuery] int teacherId)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return NotFound("Курс не найден");

            var teacher = await _context.Teachers.FindAsync(teacherId);
            if (teacher == null)
                return NotFound("Преподаватель не найден");

            // Назначаем преподавателя для курса
            course.TeacherId = teacherId;
            course.Teacher = teacher;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
