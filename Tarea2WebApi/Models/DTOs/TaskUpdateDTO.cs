using System.ComponentModel.DataAnnotations;

namespace Tarea2WebApi.Models.DTOs
{
    public class TaskUpdateDTO
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }


    }
}
