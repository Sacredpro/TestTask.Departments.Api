using Microsoft.EntityFrameworkCore;
using TestTask.Departments.Domain.DbEntities;

namespace TestTask.Departments.Domain
{
    public interface IApplicationContext
    {
        DbSet<Department> Departments { get; set; }
        DbSet<EmployeePosition> EmployeePositions { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<Position> Positions { get; set; }

        Task<Department> AddDepartmentAsync(Department department, CancellationToken cancellationToken = default);
        Task<Employee> AddEmployeeAsync(Employee employee, CancellationToken cancellationToken = default);
        Task<EmployeePosition> AddEmployeePositionAsync(EmployeePosition employeePosition, CancellationToken cancellationToken = default);
        Task<Position> AddPositionAsync(Position position, CancellationToken cancellationToken = default);
        Task<Department> GetDepartmentByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Department> GetDepartmentByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<List<Department>> GetDepartmentsAsync(CancellationToken cancellationToken = default);
        Task<Employee> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Employee> GetEmployeeByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<EmployeePosition> GetEmployeePositionByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<EmployeePosition>> GetEmployeePositionsAsync(CancellationToken cancellationToken = default);
        Task<List<Employee>> GetEmployeesAsync(CancellationToken cancellationToken = default);
        Task<Position> GetPositionByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Position> GetPositionByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<List<Position>> GetPositionsAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync();
        Task<Employee> UpdateEmployeeAsync(Employee employee, CancellationToken cancellationToken = default);
    }
}