
namespace ECommerece.Domain.Entities;

public class Payment
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order? Order { get; set; }
    
    public decimal Amount { get; set; }
    
    public DateTime PaymentDate { get; set; }

    public string TransactionReference { get; set; } = string.Empty;
    
    public bool IsSuccess { get; set; }
}
