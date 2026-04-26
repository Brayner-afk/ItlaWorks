using System.ComponentModel.DataAnnotations;

namespace ProductApi.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    [Range(0.01, 10000)]
    public decimal Price { get; set; }
    
    public int Stock { get; set; }
}
