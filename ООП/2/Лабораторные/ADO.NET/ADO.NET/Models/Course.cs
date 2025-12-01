using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET.Models
{
    public class Course
    {
        public string CourseName { get; set; }
        public string Category { get; set; }
        public int Lessons { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }

    public class CourseUpdateModel
    {
        public Course UpdatedCourse { get; set; }
        public string OriginalName { get; set; }
    }


}
