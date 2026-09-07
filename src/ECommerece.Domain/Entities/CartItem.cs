using ECommerece.Domain.Entities;

namespace ECommerce.Domain.Entities;

public class CartItem
{
    public int Id { get; private set; }

    public int CartId { get; private set; }

    public int ProductId { get; private set; }

    public int Quantity { get; private set; }

    public DateTime AddedAt { get; private set; }

    // Navigation Property
    public Cart Cart { get; private set; } = null!;

    private CartItem()
    {
    }

    public CartItem(
        int cartId,
        int productId,
        int quantity)
    {
        CartId = cartId;
        ProductId = productId;
        Quantity = quantity;
        AddedAt = DateTime.UtcNow;
    }

    public bool IsExpired(
        DateTime utcNow,
        TimeSpan expirationTime)
    {
        return AddedAt.Add(expirationTime) <= utcNow;
    }
}