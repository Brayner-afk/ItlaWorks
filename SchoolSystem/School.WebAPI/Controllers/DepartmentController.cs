using Microsoft.AspNetCore.Mvc;
using School.Application.Contract;
using School.Application.Dtos.Department;

namespace School.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase
{
    private readonly IDepartamentService _departmentService;

    public DepartmentController(IDepartamentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _departmentService.GetAll();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(DepartmentDto dto)
    {
        var result = await _departmentService.Save(dto);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }
}
