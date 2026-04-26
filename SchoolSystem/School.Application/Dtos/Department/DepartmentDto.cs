namespace School.Application.Dtos.Department;

public class DepartmentDto : DtoBase
{
    public string Name { get; set; } = string.Empty;
    public decimal Budget { get; set; }
}
