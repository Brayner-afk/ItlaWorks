namespace School.Application.Core;

public class ServiceResult
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public object? Data { get; set; }
}

public interface IBaseService<TDto> where TDto : class
{
    Task<ServiceResult> GetAll();
    Task<ServiceResult> GetById(int id);
    Task<ServiceResult> Save(TDto dto);
    Task<ServiceResult> Update(TDto dto);
}

public abstract class BaseService<TDto> : IBaseService<TDto> where TDto : class
{
    public abstract Task<ServiceResult> GetAll();
    public abstract Task<ServiceResult> GetById(int id);
    public abstract Task<ServiceResult> Save(TDto dto);
    public abstract Task<ServiceResult> Update(TDto dto);
}
