using ECommerece.Domain.Enum;

namespace ECommerece.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public decimal Subtotal { get; set; }
    
    public decimal DiscountAmount { get; set; }
   
    public decimal TaxAmount { get; set; }
    
    public decimal ShippingFee { get; set; }
    
    public decimal TotalAmount { get; set; }
    
    public List<OrderItem> Items { get; set; } = new();
    public Payment? Payment { get; set; }
}
