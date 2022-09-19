namespace TestTask.Departments.Domain.DbEntities
{
    public class EmployeePosition
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public int PositionId { get; set; }
        public int DepartmentId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
