using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using GenchoFit.Data;
using GenchoFit.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GenchoFit.Controllers;

public class AccountController(ShopDbContext db) : Controller
{
    [HttpGet] public IActionResult Register() => View(new RegisterModel());
    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var email = model.Email.Trim().ToLowerInvariant();
        if (await db.Users.AnyAsync(u => u.Email == email)) { ModelState.AddModelError("Email", "Този email вече има акаунт."); return View(model); }
        var user = new User { Email = email, Name = model.Name.Trim(), PasswordHash = Hash(model.Password) };
        db.Users.Add(user); await db.SaveChangesAsync(); await SignIn(user); return RedirectToAction("Profile");
    }
    [HttpGet] public IActionResult Login(string? returnUrl = null) => View(new LoginModel { ReturnUrl = returnUrl });
    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email.Trim().ToLowerInvariant());
        if (user is null || user.PasswordHash != Hash(model.Password)) { ModelState.AddModelError("", "Грешен email или парола."); return View(model); }
        await SignIn(user); return LocalRedirect(string.IsNullOrWhiteSpace(model.ReturnUrl) ? "/Account/Profile" : model.ReturnUrl);
    }
    [HttpPost] public async Task<IActionResult> Logout() { await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); return RedirectToAction("Index", "Shop"); }
    [Authorize] public async Task<IActionResult> Profile() => View(await db.Users.FirstAsync(u => u.Email == User.Identity!.Name));
    private async Task SignIn(User user) { var claims = new[] { new Claim(ClaimTypes.Name, user.Email), new Claim("display_name", user.Name) }; await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme))); }
    private static string Hash(string value) => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value)));
}
public class RegisterModel { [Required, StringLength(80)] public string Name { get; set; } = ""; [Required, EmailAddress] public string Email { get; set; } = ""; [Required, MinLength(6)] public string Password { get; set; } = ""; }
public class LoginModel { [Required, EmailAddress] public string Email { get; set; } = ""; [Required] public string Password { get; set; } = ""; public string? ReturnUrl { get; set; } }
