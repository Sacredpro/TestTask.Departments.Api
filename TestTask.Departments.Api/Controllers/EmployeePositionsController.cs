using Microsoft.AspNetCore.Mvc;
using TestTask.Departments.Domain.DbEntities;
using TestTask.Departments.Domain;

namespace TestTask.Departments.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeePositionsController : ControllerBase
    {
        private readonly IApplicationContext _dbcontext;

        public EmployeePositionsController(IApplicationContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet("{id}")]
        public async Task<EmployeePosition> Get(int id)
        {
            var employee = await _dbcontext.GetEmployeePositionByIdAsync(id);

            if (employee == null)
            {
                throw new InvalidDataException($"Id {id} not found");
            }

            return employee;
        }

        [HttpPost("{Employee}")]
        public async Task<EmployeePosition> Post(EmployeePosition employeePosition)
        {
            await _dbcontext.AddEmployeePositionAsync(employeePosition);
            await _dbcontext.SaveChangesAsync();
            return employeePosition;
        }

        [HttpPut("{Employee}")]
        public async Task<EmployeePosition> Update(EmployeePosition employeePosition)
        {
            var entry = await _dbcontext.GetEmployeePositionByIdAsync(employeePosition.Id);

            if (entry == null)
            {
                throw new InvalidDataException($"Id {employeePosition.Id} not found");
            }

            entry.PositionId = employeePosition.Id;
            entry.DepartmentId = employeePosition.DepartmentId;
            entry.EmployeeId = employeePosition.EmployeeId;
            await _dbcontext.SaveChangesAsync();
            return entry;
        }

        [HttpDelete("{id}")]
        public async Task<EmployeePosition> Delete(int id)
        {
            var entry = await _dbcontext.GetEmployeePositionByIdAsync(id);

            if (entry == null)
            {
                throw new InvalidDataException($"Id {id} not found");
            }

            if (entry.EmployeeId == null)
            {
                throw new InvalidDataException($"Id {id} not found");
            }
            entry.IsDeleted = true;
            await _dbcontext.SaveChangesAsync();
            return entry;
        }

    }
}
