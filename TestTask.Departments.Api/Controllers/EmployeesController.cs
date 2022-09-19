using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTask.Departments.Domain;
using TestTask.Departments.Domain.DbEntities;

namespace TestTask.Departments.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IApplicationContext _dbcontext;

        public EmployeesController(IApplicationContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet("{id}")]
        public async Task<Employee> Get(int id)
        {
            var employee = await _dbcontext.GetEmployeeByIdAsync(id);

            if (employee==null)
            {
                throw new InvalidDataException($"Id {id} not found");
            }

            return employee;
        }

        [HttpPost("{Employee}")]
        public async Task<Employee> Post(Employee employee)
        {
            await _dbcontext.AddEmployeeAsync(employee);
            await _dbcontext.SaveChangesAsync();
            return employee;
        }

        [HttpPut("{Employee}")]
        public async Task<Employee> Update(Employee employee)
        {
            var entry = await _dbcontext.GetEmployeeByIdAsync(employee.Id);

            if (entry == null)
            {
                throw new InvalidDataException($"Id {employee.Id} not found");
            }

            entry.Name = employee.Name;
            await _dbcontext.SaveChangesAsync();
            return entry;
        }

        [HttpDelete("{id}")]
        public async Task<Employee> Delete(int id)
        {
            var entry = await _dbcontext.GetEmployeeByIdAsync(id);

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
