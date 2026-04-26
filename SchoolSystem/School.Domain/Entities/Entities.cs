using School.Domain.Core;

namespace School.Domain.Entities;

public class Student : Person { }

public class Instructor : Person { }

public class Course : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public int Credits { get; set; }
    public int DepartmentId { get; set; }
}

public class Department : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public decimal Budget { get; set; }
    public DateTime StartDate { get; set; }
    public int? InstructorId { get; set; }
}
