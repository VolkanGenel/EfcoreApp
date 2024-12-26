using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EfcoreApp.Data
{
    public class CourseRegistration
    {
        [Key]
        public int RegistrationId { get; set; }
        public int StudentId { get; set; }//StudentId alanı ve CourseId alanını hiç girmeseydik de olurdu. Course Entity'sinde öyle yaptım.
        public Student Student { get; set; } = null!;
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }
        

    }
}