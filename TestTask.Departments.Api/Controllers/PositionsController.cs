using Microsoft.AspNetCore.Mvc;
using TestTask.Departments.Domain;
using TestTask.Departments.Domain.DbEntities;

namespace TestTask.Departments.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PositionsController : ControllerBase
    {
        private readonly IApplicationContext _dbcontext;

        public PositionsController(IApplicationContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet("{id}")]
        public async Task<Position> Get(int id)
        {
            var position = await _dbcontext.GetPositionByIdAsync(id);

            if (position == null)
            {
                throw new InvalidDataException($"Id {id} not found");
            }

            return position;
        }

        [HttpPost("{Position}")]
        public async Task<Position> Post(Position position)
        {
            await _dbcontext.AddPositionAsync(position);
            await _dbcontext.SaveChangesAsync();
            return position;
        }

        [HttpPut("{Position}")]
        public async Task<Position> Update(Position position)
        {
            var entry = await _dbcontext.GetPositionByIdAsync(position.Id);

            if (entry == null)
            {
                throw new InvalidDataException($"Id {position.Id} not found");
            }

            entry.Name = position.Name;
            await _dbcontext.SaveChangesAsync();
            return entry;
        }

        [HttpDelete("{id}")]
        public async Task<Position> Delete(int id)
        {
            var entry = await _dbcontext.GetPositionByIdAsync(id);

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
