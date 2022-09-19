using Microsoft.EntityFrameworkCore;

namespace TestTask.Departments.Domain.DbEntities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentDepartmentId { get; set; }
        public bool IsDeleted { get; set; }

    }
}
