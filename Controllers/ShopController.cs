using GenchoFit.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GenchoFit.Controllers;

public class ShopController(ShopDbContext db) : Controller
{
    public async Task<IActionResult> Index(string? search)
    {
        var products = db.Products.AsQueryable();
        if (!string.IsNullOrWhiteSpace(search)) products = products.Where(p => p.Name.Contains(search) || p.Category.Contains(search));
        ViewBag.Search = search;
        return View(await products.OrderBy(p => p.Id).ToListAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await db.Products.FindAsync(id);
        return product is null ? NotFound() : View(product);
    }
}
