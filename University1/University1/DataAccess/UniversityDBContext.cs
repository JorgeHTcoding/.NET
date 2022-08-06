using Microsoft.EntityFrameworkCore;
using University1.Models.DataModels;

namespace University1.DataAccess
{
    public class UniversityDBContext : DbContext
    {
        public UniversityDBContext(DbContextOptions<UniversityDBContext> options) : base(options)
        {

        }
        public DbSet<User>? Users { get; set; }
        public DbSet<Course>? Courses { get; set; }
        public DbSet<Chapters>? Chapters { get; set; }
        public DbSet<Category>? Categorias { get; set; }
        public DbSet<Student>? Students { get; set; }
    }
}
