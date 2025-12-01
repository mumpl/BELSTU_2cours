using LinguaBender.Models;

namespace LinguaBender.Services
{
    public interface ICourseService
    {
        Task<List<Course>> GetAllCoursesAsync();
        Task AddCourseAsync(Course course);
        Task DeleteCourseAsync(string courseName);
    }
}
