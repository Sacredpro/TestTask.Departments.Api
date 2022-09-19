using TestTask.Departments.Api.Dto;

namespace TestTask.Departments.Api
{
    public interface IDataParser
    {
        List<ExcelRowDto> ParseExcelToList(IFormFile file);
    }
}