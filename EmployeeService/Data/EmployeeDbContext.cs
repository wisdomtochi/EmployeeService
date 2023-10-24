using EmployeeService.Domains;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Data
{
    public class EmployeeDbContext : DbContext
    {
        //public EmployeeDbContext() { }
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
            : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<ConnectionRequest> ConnectionRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
               .HasMany(e => e.Connections)
               .WithMany(e => e.Employees);

            //modelBuilder.Entity<Employee>()
            //.HasMany(e => e.Requests);
            //.WithOne(e => e.ReceiverId);
        }


    }
}
