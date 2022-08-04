using Microsoft.EntityFrameworkCore;
using univeristy_api_backend.Models.DataModels;

namespace univeristy_api_backend.DataAccess
{
    public class UniversityDBContext : DbContext
    {
        public UniversityDBContext(DbContextOptions<UniversityDBContext> options) : base(options)
        {

        }
        public DbSet<User>? User { get; set; }
        public DbSet<Curso>? Cursos { get; set; }
    }
}
