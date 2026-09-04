
namespace ECommerece.Domain.Entities;

public class Customer
{
    public int Id { get; set; }
    
   
    public string FullName { get; set; } = string.Empty;
   
    public string Email { get; set; } = string.Empty;
    
    public bool IsVip { get; set; }
    
    public List<Order> Orders { get; set; } = new();
}
