using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Data;
using System.Threading.Tasks;
using TestTask.Departments.Domain;

namespace TestTask.Departments.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganizationalStructureController : ControllerBase
    { 
        private readonly ILogger<OrganizationalStructureController> _logger;
        private readonly IApplicationContext _dbcontext;
        private readonly IDbImporter _dbImporter;
        private readonly IDataParser _dataParser;
        
        public OrganizationalStructureController(ILogger<OrganizationalStructureController> logger, IApplicationContext dbcontext, IDbImporter dbImporter, IDataParser dataParser)
        {
            _logger = logger;
            _dbcontext = dbcontext;
            _dbImporter = dbImporter;
            _dataParser = dataParser;
        }

        [HttpPost]
        public async Task<IActionResult> Post(IFormFile ExcelDb)
        {  
            var excelToList = _dataParser.ParseExcelToList(ExcelDb);
            await _dbImporter.ImportListToDbAsync(excelToList);        
            
            return Ok($"Received file {ExcelDb.FileName} with size in bytes {ExcelDb.Length}");
        }
    }
}
