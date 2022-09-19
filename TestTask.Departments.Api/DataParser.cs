using ExcelDataReader;
using System.Data;
using TestTask.Departments.Api.Dto;

namespace TestTask.Departments.Api
{
    public class DataParser : IDataParser
    {
        public List<ExcelRowDto> ParseExcelToList(IFormFile file)
        {
            List<ExcelRowDto> excelRowDtos = new List<ExcelRowDto>();

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var reader = ExcelReaderFactory.CreateReader(file.OpenReadStream()))
            {
                var result = reader.AsDataSet();

                foreach (DataTable dt in result.Tables)
                {
                    for (int i = 1; i < dt.Rows.Count; i++)
                    {
                        var employee = new ExcelRowDto
                        {
                            Department = dt.Rows[i][0].ToString(),
                            ParentDepartment = dt.Rows[i][1].ToString(),
                            Position = dt.Rows[i][2].ToString(),
                            Name = dt.Rows[i][3].ToString()
                        };
                        excelRowDtos.Add(employee);
                    }

                }

            }
            return excelRowDtos;
        }
    }
}

