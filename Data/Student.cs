using System.ComponentModel.DataAnnotations;

namespace EfcoreApp.Data
{
    public class Student
    {
        // id => primary key (Eğer Id ya da StudentId yerine başka bir isim yazsaydım Key notasyonuna ihtiyacım olurdu.)
        public int StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? StudentSurname { get; set; }
        public string NameSurname {
            get
            {
                return this.StudentName+" "+this.StudentSurname;
            }
        }
        public string? EMail { get; set; }
        public string? PhoneNumber { get; set; }
        public ICollection<CourseRegistration> CourseRegistrations { get; set; } = new List<CourseRegistration>();
    }
}