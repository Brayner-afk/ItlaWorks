using School.Domain.Entities;

namespace School.Infrastructure.Interfaces;

public interface ICourseRepository
{
    Task<IEnumerable<Course>> GetAllAsync();
    Task<Course?> GetByIdAsync(int id);
    Task AddAsync(Course course);
}

public interface IDepartmentRepository
{
    Task<IEnumerable<Department>> GetAllAsync();
    Task AddAsync(Department department);
}
