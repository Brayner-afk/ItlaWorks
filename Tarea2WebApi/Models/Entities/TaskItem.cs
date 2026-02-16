using System.ComponentModel.DataAnnotations;

namespace Tarea2WebApi.Models.Entities
{
    public class TaskItem
    {
 
            public int Id { get; set; }

            [Required]
            public string Title { get; set; } = string.Empty;

            [Required]
            public string Description { get; set; } = string.Empty;

            public bool IsCompleted { get; set; }
        


    }
}
