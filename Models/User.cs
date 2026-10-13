using System.ComponentModel.DataAnnotations;

namespace GenchoFit.Models;

public class User
{
    public int Id { get; set; }
    [Required, EmailAddress, StringLength(160)] public string Email { get; set; } = "";
    [Required, StringLength(80)] public string Name { get; set; } = "";
    [Required] public string PasswordHash { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
