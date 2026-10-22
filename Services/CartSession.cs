using System.Text.Json;

namespace GenchoFit.Services;

public sealed record CartLine(int ProductId, int Quantity);

public static class CartSession
{
    private const string Key = "genchofit-cart";
    public static List<CartLine> GetCart(this ISession session) => JsonSerializer.Deserialize<List<CartLine>>(session.GetString(Key) ?? "[]") ?? [];
    public static void SaveCart(this ISession session, List<CartLine> cart) => session.SetString(Key, JsonSerializer.Serialize(cart.Where(x => x.Quantity > 0).ToList()));
    public static int CartCount(this ISession session) => session.GetCart().Sum(x => x.Quantity);
    public static void AddToCart(this ISession session, int productId)
    {
        var cart = session.GetCart();
        var index = cart.FindIndex(x => x.ProductId == productId);
        if (index < 0) cart.Add(new CartLine(productId, 1)); else cart[index] = cart[index] with { Quantity = cart[index].Quantity + 1 };
        session.SaveCart(cart);
    }
    public static void RemoveFromCart(this ISession session, int productId) => session.SaveCart(session.GetCart().Where(x => x.ProductId != productId).ToList());
}
