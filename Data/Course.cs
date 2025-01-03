using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace EfcoreApp.Data
{
    public class Course
    {
        public int CourseId { get; set; }
        public string? Title { get; set; }
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; } = null!;
        public ICollection<CourseRegistration> CourseRegistrations { get; set; } = new List<CourseRegistration>();
    }
}