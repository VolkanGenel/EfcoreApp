using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder.Extensions;

namespace EfcoreApp.Data
{
    public class Instructor
    {
        [Key]
        public int InstructorId { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? EMail { get; set; }
        public string? PhoneNumber { get; set; }
        [Display(Name = "Instructor Name and Surname")]
        public string NameSurname {
            get
            {
                return this.Name+" "+this.Surname;
            }
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode =false)]
        public DateTime StartDate { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();

    }
}