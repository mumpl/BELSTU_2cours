using LinguaBender.Data;
using LinguaBender.Models;

namespace LinguaBender.UndoRedo
{
    public class DeleteCourseCommand : ICommandAction
    {
        private readonly AppDbContext _context;
        private readonly Course _course;

        public DeleteCourseCommand(AppDbContext context, Course course)
        {
            _context = context;
            _course = course;
        }

        public void Execute()
        {
            _context.Courses.Remove(_course);
            _context.SaveChanges();
        }

        public void UnExecute()
        {
            _context.Courses.Add(_course);
            _context.SaveChanges();
        }
    }
}
