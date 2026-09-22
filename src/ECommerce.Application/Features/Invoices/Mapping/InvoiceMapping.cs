using ECommerce.Application.Features.Invoices.DTO;

namespace ECommerce.Application.Features.Invoices.Mapping;

public static class InvoiceMapping
{
    public static InvoiceDto ToInvoiceDto(this ECommerece.Domain.Entities.Order order)
    {
        return new InvoiceDto
        {
            OrderId = order.Id,

            CustomerName = order.Customer?.FullName ?? string.Empty,

            CustomerEmail = order.Customer?.Email ?? string.Empty,

            CreatedAt = order.CreatedAt,

            Status = order.Status.ToString(),

            Subtotal = order.Subtotal,

            DiscountAmount = order.DiscountAmount,

            TaxAmount = order.TaxAmount,

            ShippingFee = order.ShippingFee,

            TotalAmount = order.TotalAmount,

            Items = order.Items
                .Select(item => new InvoiceItemDto
                {
                    ProductName = item.Product?.Name ?? string.Empty,

                    Quantity = item.Quantity,

                    UnitPrice = item.UnitPrice,

                    Total = item.Quantity * item.UnitPrice
                })
                .ToList()
        };
    }
}