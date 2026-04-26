using Microsoft.EntityFrameworkCore;
using School.Application.Contract;
using School.Application.Core;
using School.Application.Dtos.Department;
using School.Domain.Entities;
using School.Infrastructure.Context;

namespace School.Application.Service;

public class DepartamentService : IDepartamentService
{
    private readonly SchoolContext _context;

    public DepartamentService(SchoolContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult> GetAll()
    {
        var result = new ServiceResult();
        try
        {
            result.Data = await _context.Departments.ToListAsync();
            result.Success = true;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = "Error obteniendo departamentos: " + ex.Message;
        }
        return result;
    }

    public async Task<ServiceResult> GetById(int id)
    {
        var result = new ServiceResult();
        var dept = await _context.Departments.FindAsync(id);
        if (dept == null)
        {
            result.Success = false;
            result.Message = "Departamento no encontrado";
        }
        else
        {
            result.Data = dept;
            result.Success = true;
        }
        return result;
    }

    public async Task<ServiceResult> Save(DepartmentDto dto)
    {
        var result = new ServiceResult();
        
        // VALIDACIONES
        if (string.IsNullOrEmpty(dto.Name))
        {
            result.Success = false;
            result.Message = "El nombre del departamento es requerido.";
            return result;
        }
        if (dto.Budget <= 0)
        {
            result.Success = false;
            result.Message = "El presupuesto debe ser mayor a cero.";
            return result;
        }

        try
        {
            var entity = new Department { Name = dto.Name, Budget = dto.Budget };
            await _context.Departments.AddAsync(entity);
            await _context.SaveChangesAsync();
            result.Success = true;
            result.Message = "Departamento guardado correctamente.";
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = "Error al guardar: " + ex.Message;
        }
        return result;
    }

    public async Task<ServiceResult> Update(DepartmentDto dto)
    {
        var result = new ServiceResult();
        var entity = await _context.Departments.FindAsync(dto.Id);
        if (entity == null)
        {
            result.Success = false;
            result.Message = "Departamento no existe.";
            return result;
        }

        entity.Name = dto.Name;
        entity.Budget = dto.Budget;

        await _context.SaveChangesAsync();
        result.Success = true;
        return result;
    }
}
