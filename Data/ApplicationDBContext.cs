using DemoWebAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace DemoWebAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        // Define DbSet properties for your entities
        public DbSet<TblEmployee> Employees { get; set; }
        public DbSet<TblDesignation> Designations { get; set; }
    }
}
