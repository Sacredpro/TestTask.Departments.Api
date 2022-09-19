using TestTask.Departments.Domain;
using TestTask.Departments.Api.Dto;
using TestTask.Departments.Domain.DbEntities;

namespace TestTask.Departments.Api
{
    public class DbImporter : IDbImporter
    {
        private readonly IApplicationContext _dbcontext;
        public DbImporter(IApplicationContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task ImportListToDbAsync(List<ExcelRowDto> list)
        {
            IEnumerable<ExcelRowDto> distinctDepartments = list.DistinctBy(x => x.Department);
            IEnumerable<ExcelRowDto> distinctPositions = list.DistinctBy(x => x.Position);

            foreach (var c in distinctDepartments)
            {
                _dbcontext.Departments.Add(new Department { Name = c.Department });
            }

            foreach (var c in distinctPositions)
            {
                _dbcontext.Positions.Add(new Position { Name = c.Position });
            }

            foreach (var c in list)
            {
                _dbcontext.Employees.Add(new Employee { Name = c.Name });
            }

            await _dbcontext.SaveChangesAsync();

            foreach (var c in distinctDepartments)
            {
                if (!string.IsNullOrWhiteSpace(c.ParentDepartment))
                {
                    var department = await _dbcontext.GetDepartmentByNameAsync(c.Department);
                    var parentDepartment = await _dbcontext.GetDepartmentByNameAsync(c.ParentDepartment);

                    department.ParentDepartmentId = parentDepartment.Id;
                }

            }

            foreach (var c in list)
            {
                var position = await _dbcontext.GetPositionByNameAsync(c.Position);
                var employee = await _dbcontext.GetEmployeeByNameAsync(c.Name);
                var department = await _dbcontext.GetDepartmentByNameAsync(c.Department);

                await _dbcontext.EmployeePositions.AddAsync(new EmployeePosition { PositionId = position.Id, EmployeeId = employee.Id, DepartmentId = department.Id });
            }

            await _dbcontext.SaveChangesAsync();
        }
    }
}
