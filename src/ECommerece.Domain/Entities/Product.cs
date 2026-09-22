
namespace ECommerece.Domain.Entities;

public class Product
{
    public int Id { get; set; }

   public string Name { get;  set; } = string.Empty;
   public string SKU { get;  set; } = string.Empty;
   public decimal Price { get; private set; }
   public int StockQuantity { get; private set; }
   public int ViewCount { get; private set; }
    public void SetPrice(decimal price)
    {
        if (price <= 0)
            throw new ArgumentException(
                "Product price must be greater than zero.");

        Price = price;
    }

    public void SetStockQuantity(int quantity)
    {
        if (quantity < 0)
            throw new ArgumentException(
                "Stock quantity cannot be negative.");

        StockQuantity = quantity;
    }
    public void IncreaseViewCount(int count)
    {
        if (count <= 0)
            return;

        ViewCount += count;
    }
}
