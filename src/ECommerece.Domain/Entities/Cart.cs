using ECommerece.Domain.Entities;

namespace ECommerce.Domain.Entities;

public class Cart
{
    public int Id { get; private set; }

    public int UserId { get; private set; }

    private readonly List<CartItem> _items = new();

    public IReadOnlyCollection<CartItem> Items
        => _items.AsReadOnly();

    public Customer customer { get; private set; } = null!;

    private Cart()
    {
    }

    public Cart(int userId)
    {
        UserId = userId;
    }

    public void AddItem(CartItem item)
    {
        _items.Add(item);
    }

    public void RemoveItem(CartItem item)
    {
        _items.Remove(item);
    }
}