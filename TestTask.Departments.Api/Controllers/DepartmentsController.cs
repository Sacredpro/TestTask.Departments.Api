using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTask.Departments.Api.Dto;
using TestTask.Departments.Domain;
using TestTask.Departments.Domain.DbEntities;

namespace TestTask.Departments.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IApplicationContext _dbcontext;
        public DepartmentsController(IApplicationContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet("{id}/childs")]
        public async Task<List<Department>> GetDepartmentsByParentId(int id)
        {
            var result = await _dbcontext.Departments.Where(x => x.ParentDepartmentId == id).ToListAsync();
            return result;
        }

        [HttpGet("{id}/employees")]
        public async Task<DepartmentEmpoloyeesAmountDto> GetEmployeesAmountByDepartmentId(int id)
        {
            //количество сотрудников и количество ползиций в этом отделе
            
            var amountOfEmployees = await _dbcontext.EmployeePositions.Where(x => x.DepartmentId == id).CountAsync();
            var amountOfPositions = await _dbcontext.EmployeePositions
                .Where(x => x.DepartmentId == id)
                .GroupBy(y => y.PositionId)
                .CountAsync();

            return new DepartmentEmpoloyeesAmountDto { AmountOfEmployees = amountOfEmployees, AmountOfPositions = amountOfPositions };
        }

        [HttpGet("{id}")]
        public async Task<Department> Get(int id)
        {
            var department = await _dbcontext.GetDepartmentByIdAsync(id);

            if (department == null)
            {
                throw new InvalidDataException($"Id {id} not found");
            }

            return department;
        }

        [HttpPost("{Department}")]
        public async Task<Department> Post(Department department)
        {
            await _dbcontext.AddDepartmentAsync(department);
            await _dbcontext.SaveChangesAsync();
            return department;
        }

        [HttpPut("{Department}")]
        public async Task<Department> Update(Department department)
        {
            var entry = await _dbcontext.GetDepartmentByIdAsync(department.Id);

            if (entry == null)
            {
                throw new InvalidDataException($"Id {department.Id} not found");
            }

            entry.Name = department.Name;
            await _dbcontext.SaveChangesAsync();
            return entry;
        }

        [HttpDelete("{id}")]
        public async Task<Department> Delete(int id)
        {
            var entry = await _dbcontext.GetDepartmentByIdAsync(id);

            if (entry == null)
            {
                throw new InvalidDataException($"Id {id} not found");
            }

            entry.IsDeleted = true;
            await _dbcontext.SaveChangesAsync();
            return entry;
        }
    }
}
