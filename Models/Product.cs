using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GenchoFit.Models;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required, StringLength(100)] public string Name { get; set; } = "";
    [Required, StringLength(50)] public string Category { get; set; } = "";
    [Range(typeof(decimal), "0", "10000")]
    [Precision(18, 2)]
    public decimal Price { get; set; }
    [StringLength(500)] public string Description { get; set; } = "";
    [Required] public string Emoji { get; set; } = "💪";
}
