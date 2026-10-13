using System.ComponentModel.DataAnnotations;

namespace GenchoFit.Models;

public class Product
{
    public int Id { get; set; }
    [Required, StringLength(100)] public string Name { get; set; } = "";
    [Required, StringLength(50)] public string Category { get; set; } = "";
    [Range(0, 10000)] public decimal Price { get; set; }
    [StringLength(500)] public string Description { get; set; } = "";
    public string Emoji { get; set; } = "💪";
}
