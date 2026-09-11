namespace ECommerce.Application.Features.Invoices.DTO;

public class InvoiceItemDto
{
    public string ProductName { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Total { get; set; }
}