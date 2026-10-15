using System.ComponentModel.DataAnnotations;

namespace GenchoFit.Models;

public class RegisterModel
{
    [Required, StringLength(80)] public string Name { get; set; } = "";
    [Required, EmailAddress] public string Email { get; set; } = "";
    [Required, MinLength(6)] public string Password { get; set; } = "";
}

public class LoginModel
{
    [Required, EmailAddress] public string Email { get; set; } = "";
    [Required] public string Password { get; set; } = "";
    public string? ReturnUrl { get; set; }
}
