using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinguaBender.Models;

namespace LinguaBender.Data.UnitOfWork
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        Task<Course?> GetByNameAsync(string name);
    }
}
