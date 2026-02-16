using Microsoft.AspNetCore.Mvc;
using Tarea2WebApi.Data;
using Tarea2WebApi.Models.DTOs;
using Tarea2WebApi.Models.Entities;

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskDbContext _context;

        public TasksController(TaskDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var tasks = _context.Tasks.ToList();


            return Ok(tasks);
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var task = _context.Tasks.Find(id);

            if (task == null)
                return NotFound();

            return Ok(task);
        }


        [HttpPost]
        public IActionResult Post(TaskCreateDTO dto)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                IsCompleted = dto.IsCompleted
            };

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = task.Id }, task);
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, TaskUpdateDTO dto)
        {
            var task = _context.Tasks.Find(id);

            if (task == null)
                return NotFound();

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.IsCompleted = dto.IsCompleted;

            _context.SaveChanges();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var task = _context.Tasks.Find(id);

            if (task == null)
            {
                return NotFound();
            }
            _context.Tasks.Remove(task);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
