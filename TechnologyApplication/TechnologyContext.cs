using Microsoft.EntityFrameworkCore;
using TechnologyApplication.Models;

namespace TechnologyApplication
{
    public class TechnologyContext : DbContext
    {
        public TechnologyContext() { }

        public TechnologyContext(DbContextOptions<TechnologyContext> options) : base(options)
        {

        }

        public static bool isMigration = true;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
            if (isMigration)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-HU6GJD4;Database=CompanyDbName;Trusted_Connection=True;MultipleActiveResultSets=true;User Id=sa; Password=13491a03g3");

            }
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}
