using Microsoft.EntityFrameworkCore;
using TcHrTest.Models;

namespace TcHrTest.Data
{
    public class HrContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public HrContext(DbContextOptions<HrContext> options): base(options) {} 
    }

    public class DatabaseInitializer
    {
        public static void Initialize(HrContext context) {
            context.Database.EnsureCreated();

            context.Positions.Add(new Position { Level = 1, Title = "Analyst", SalaryRange = new Range { High = 60000, Low = 50000 } });
            context.Positions.Add(new Position { Level = 2, Title = "Intermediate Analyst", SalaryRange = new Range { High = 70000, Low = 60000 } });
            context.Positions.Add(new Position { Level = 3, Title = "Senior Analyst", SalaryRange = new Range { High = 80000, Low = 70000 } });
            context.Positions.Add(new Position { Level = 4, Title = "Manager", SalaryRange = new Range { High = 90000, Low = 80000 } });
            context.Positions.Add(new Position { Level = 5, Title = "Director", SalaryRange = new Range { High = 100000, Low = 90000 } });
            context.Positions.Add(new Position { Level = 6, Title = "Vice President", SalaryRange = new Range { High = 110000, Low = 100000 } });
            context.Positions.Add(new Position { Level = 7, Title = "Officer", SalaryRange = new Range { High = 120000, Low = 110000 } });

            context.Departments.Add(new Department { Id = 1, Name = "Engineering" });
            context.Departments.Add(new Department { Id = 2, Name = "Support" });
            context.Departments.Add(new Department { Id = 3, Name = "Professional Services" });
            context.Departments.Add(new Department { Id = 4, Name = "Sales" });
            context.Departments.Add(new Department { Id = 5, Name = "Human Resources" });
            context.Departments.Add(new Department { Id = 6, Name = "Executive" });

            context.Employees.Add(new Employee { Name = string.Empty});
            context.Employees.Add(new Employee { Name = "Chad", ManagerId = 1, DepartmentId = 1, PositionId = 1 });
            context.Employees.Add(new Employee { Name = "Rosie", ManagerId = 1, DepartmentId = 2, PositionId = 2 });
            context.Employees.Add(new Employee { Name = "Charlie", ManagerId = 1, DepartmentId = 4, PositionId = 7 });
            context.SaveChanges();
        }
    }
}
