using TestTask.Departments.Api.Dto;

namespace TestTask.Departments.Api
{
    public interface IDbImporter
    {
        Task ImportListToDbAsync(List<ExcelRowDto> list);
    }
}