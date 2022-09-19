using Microsoft.EntityFrameworkCore;
using TestTask.Departments.Domain.DbEntities;

namespace TestTask.Departments.Domain
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        public DbSet<Employee> Employees { get; set; } 
        public DbSet<Position> Positions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EmployeePosition> EmployeePositions { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        //Employees
        public async Task<List<Employee>> GetEmployeesAsync(CancellationToken cancellationToken = new())
        {
            var employees = await Employees.Where(x => !x.IsDeleted).ToListAsync(cancellationToken);
            return employees;
        }
        public async Task<Employee> GetEmployeeByNameAsync(string name, CancellationToken cancellationToken = new())
        {
            var employee = await Employees.FirstOrDefaultAsync(x => x.Name == name && !x.IsDeleted, cancellationToken);
            return employee;
        }
        public async Task<Employee> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken = new())
        {
            var employee = await Employees.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
            return employee;
        }
        public async Task<Employee> AddEmployeeAsync(Employee employee, CancellationToken cancellationToken = new())
        {
            var entry = await Employees.AddAsync(employee, cancellationToken);
            return entry.Entity;
        }
        public async Task<Employee> UpdateEmployeeAsync(Employee employee, CancellationToken cancellationToken = new())
        {
            var entry = await Employees.SingleOrDefaultAsync(x => x.Id == employee.Id);

            if (entry == null)
            {
                throw new InvalidDataException($"Id {employee.Id} not found");
            }
            entry.Name = employee.Name;
            entry.IsDeleted = employee.IsDeleted;
            return entry;
        }

        //Positions
        public async Task<List<Position>> GetPositionsAsync(CancellationToken cancellationToken = new())
        {
            var positions = await Positions.Where(x => !x.IsDeleted).ToListAsync(cancellationToken);
            return positions;
        }

        public async Task<Position> GetPositionByNameAsync(string name, CancellationToken cancellationToken = new())
        {
            var position = await Positions.FirstOrDefaultAsync(x => x.Name == name && !x.IsDeleted, cancellationToken);
            return position;
        }
        public async Task<Position> GetPositionByIdAsync(int id, CancellationToken cancellationToken = new())
        {
            var position = await Positions.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
            return position;
        }
        public async Task<Position> AddPositionAsync(Position position, CancellationToken cancellationToken = new())
        {
            var entry = await Positions.AddAsync(position, cancellationToken);
            return entry.Entity;
        }



        //Departments
        public async Task<List<Department>> GetDepartmentsAsync(CancellationToken cancellationToken = new())
        {
            var departments = await Departments.Where(x => !x.IsDeleted).ToListAsync(cancellationToken);
            return departments;
        }
        public async Task<Department> GetDepartmentByNameAsync(string name, CancellationToken cancellationToken = new())
        {
            var department = await Departments.FirstOrDefaultAsync(x => x.Name == name && !x.IsDeleted, cancellationToken);
            return department;
        }
        public async Task<Department> GetDepartmentByIdAsync(int id, CancellationToken cancellationToken = new())
        {
            var department = await Departments.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
            return department;
        }
        public async Task<Department> AddDepartmentAsync(Department department, CancellationToken cancellationToken = new())
        {
            var entry = await Departments.AddAsync(department, cancellationToken);
            return entry.Entity;
        }

        //EmployeePositions
        public async Task<List<EmployeePosition>> GetEmployeePositionsAsync(CancellationToken cancellationToken = new())
        {
            var departments = await EmployeePositions.Where(x => !x.IsDeleted).ToListAsync(cancellationToken);
            return departments;
        }
        public async Task<EmployeePosition> GetEmployeePositionByIdAsync(int id, CancellationToken cancellationToken = new())
        {
            var department = await EmployeePositions.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
            return department;
        }
        public async Task<EmployeePosition> AddEmployeePositionAsync(EmployeePosition employeePosition, CancellationToken cancellationToken = new())
        {
            var entry = await EmployeePositions.AddAsync(employeePosition, cancellationToken);
            return entry.Entity;
        }
    }
}