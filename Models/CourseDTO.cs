using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EfcoreApp.Data;

namespace EfcoreApp.Models
{
    public class CourseDTO
    {
        public int CourseId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Course Title")]
        public string? Title { get; set; }
        public int InstructorId { get; set; }
        public ICollection<CourseRegistration> CourseRegistrations { get; set; } = new List<CourseRegistration>();
    }
}