using LinguaBender.Data;
using LinguaBender.Models;
using Microsoft.Extensions.DependencyInjection;

namespace LinguaBender.UndoRedo
{
    public class AddCourseCommand : ICommandAction
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Course _course;

        public AddCourseCommand(IServiceProvider serviceProvider, Course course)
        {
            _serviceProvider = serviceProvider;
            _course = course;
        }

        public void Execute()
        {
            using var context = _serviceProvider.GetRequiredService<AppDbContext>();
            context.Courses.Add(_course);
            context.SaveChanges();
        }

        public void UnExecute()
        {
            using var context = _serviceProvider.GetRequiredService<AppDbContext>();
            var courseToRemove = context.Courses
                .FirstOrDefault(c =>
                    c.Course_Name == _course.Course_Name &&
                    c.Description == _course.Description &&
                    c.Language == _course.Language &&
                    c.Category == _course.Category &&
                    c.Price == _course.Price &&
                    c.Lessons == _course.Lessons
                );

            if (courseToRemove != null)
            {
                context.Courses.Remove(courseToRemove);
                context.SaveChanges();
            }
        }
    }
}
