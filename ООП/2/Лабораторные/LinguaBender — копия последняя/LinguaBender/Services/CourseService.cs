using LinguaBender.Data;
using LinguaBender.Data.UnitOfWork;
using LinguaBender.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LinguaBender.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Course>> GetAllCoursesAsync()
        {
            var courses = await _unitOfWork.Courses.GetAllAsync();
            return courses.ToList();
        }

        public async Task AddCourseAsync(Course course)
        {
            await _unitOfWork.Courses.AddAsync(course);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteCourseAsync(string courseName)
        {
            var course = await _unitOfWork.Courses.GetByNameAsync(courseName);
            if (course != null)
            {
                _unitOfWork.Courses.Remove(course);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        /*
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
        }*/
    }
}
