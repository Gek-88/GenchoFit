using GenchoFit.Data;
using GenchoFit.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GenchoFit.Controllers;

public class CartController(ShopDbContext db) : Controller
{
    public async Task<IActionResult> Index()
    {
        var cart = HttpContext.Session.GetCart();
        var products = await db.Products.Where(p => cart.Select(x => x.ProductId).Contains(p.Id)).ToListAsync();
        return View(cart.Join(products, x => x.ProductId, p => p.Id, (x, p) => new CartLineViewModel(p, x.Quantity)).ToList());
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(int id, string? returnUrl = null)
    {
        if (await db.Products.AnyAsync(p => p.Id == id)) HttpContext.Session.AddToCart(id);
        return !string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl) ? Redirect(returnUrl) : RedirectToAction(nameof(Index));
    }
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Remove(int id) { HttpContext.Session.RemoveFromCart(id); return RedirectToAction(nameof(Index)); }
}

public sealed record CartLineViewModel(GenchoFit.Models.Product Product, int Quantity)
{
    public decimal Total => Product.Price * Quantity;
}
