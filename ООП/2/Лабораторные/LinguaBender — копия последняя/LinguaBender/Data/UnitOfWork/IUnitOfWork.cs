using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaBender.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICourseRepository Courses { get; }
        Task<int> SaveChangesAsync();
    }
}
