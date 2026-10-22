using GenchoFit.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using GenchoFit.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromHours(12); options.Cookie.HttpOnly = true; options.Cookie.IsEssential = true; });
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => { options.LoginPath = "/Account/Login"; options.AccessDeniedPath = "/Account/Login"; });
builder.Services.AddAuthorization();
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(connection))
{
    throw new InvalidOperationException("ConnectionStrings:DefaultConnection is missing from appsettings.json.");
}
builder.Services.AddDbContext<ShopDbContext>(options =>
    options.UseMySql(connection, ServerVersion.AutoDetect(connection)));

var app = builder.Build();
if (!app.Environment.IsDevelopment()) { app.UseExceptionHandler("/Home/Error"); }
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Shop}/{action=Index}/{id?}");
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ShopDbContext>();
    db.Database.EnsureCreated();
    SeedData.AddProducts(db);
}
app.Run();
