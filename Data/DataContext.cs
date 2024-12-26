using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EfcoreApp.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<CourseRegistration> CourseRegistrations => Set<CourseRegistration>();
        public DbSet<Instructor> Instructors => Set<Instructor>();
    }
    // => iki şekilde yapılabilir Entityler üzerinden veritabanı tabloları oluşturmak ya da Sql tablolarından Entityleri çekmek
    // code-first => entity, dbcontext => database (sqlite)
    // database-first => sql server
}