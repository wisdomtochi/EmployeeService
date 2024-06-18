using EmployeeService.Domains;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Data.Context
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        { }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Connection> Connections { get; set; }
        public virtual DbSet<ConnectionRequest> ConnectionRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) => base.OnModelCreating(modelBuilder);

    }
}
