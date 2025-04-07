using _2day.Model;
using Microsoft.EntityFrameworkCore;

namespace _2day
{
    public class SchoolContext : DbContext
    {
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Определение связи многие ко многим (Student - Course)
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Students)
                .WithMany(s => s.Courses);

            // Определение связи один ко многим (Teacher - Course)
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Teacher)
                .WithMany(t => t.Courses)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            // Добавление данных в таблицы
            modelBuilder.Entity<Teacher>().HasData(
                new Teacher { TeacherId = 1, Name = "Ivanivanov" },
                new Teacher { TeacherId = 2, Name = "PetrPetrov" }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student { StudentId = 3, Name = "AnnaSmirnova" },
                new Student { StudentId = 4, Name = "SergeySidorov" }
            );

            modelBuilder.Entity<Course>().HasData(
                new Course { CourseId = 5, Title = "Mathematics", TeacherId = 1 },
                new Course { CourseId = 6, Title = "Physics", TeacherId = 2}
            );
        }
    }
}
