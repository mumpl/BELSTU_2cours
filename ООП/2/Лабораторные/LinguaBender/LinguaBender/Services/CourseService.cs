using LinguaBender.Data;
using LinguaBender.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LinguaBender.Services
{
    public class CourseService : ICourseService
    {
        private readonly IServiceProvider _serviceProvider;

        public CourseService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<List<Course>> GetAllCoursesAsync()
        {
            using var db = _serviceProvider.GetRequiredService<AppDbContext>();
            return await db.Courses.AsNoTracking().ToListAsync();
        }

        public async Task AddCourseAsync(Course course)
        {
            using var db = _serviceProvider.GetRequiredService<AppDbContext>();
            db.Courses.Add(course);
            await db.SaveChangesAsync();
        }

        public async Task DeleteCourseAsync(string courseName)
        {
            using var db = _serviceProvider.GetRequiredService<AppDbContext>();
            var course = await db.Courses.FindAsync(courseName);
            if (course != null)
            {
                db.Courses.Remove(course);
                await db.SaveChangesAsync();
            }
        }
    }
}