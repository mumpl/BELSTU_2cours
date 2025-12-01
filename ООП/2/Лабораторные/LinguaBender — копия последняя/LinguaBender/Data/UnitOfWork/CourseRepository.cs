using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinguaBender.Models;
using Microsoft.EntityFrameworkCore;

namespace LinguaBender.Data.UnitOfWork
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository

    {
        public CourseRepository(AppDbContext context) : base(context) { }

        public async Task<Course?> GetByNameAsync(string name)
        {
            return await _context.Courses.FirstOrDefaultAsync(c => c.Course_Name == name);
        }
    }
}
