using Microsoft.EntityFrameworkCore;

namespace TestTask.Departments.Domain.DbEntities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
