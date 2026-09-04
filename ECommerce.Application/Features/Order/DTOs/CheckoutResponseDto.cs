
namespace ECommerce.Application.Features.Order.DTOs
{
    public class CheckoutResponseDto
    {
        public int OrderId { get; set; }

        public string Status { get; set; } = string.Empty;

        public decimal Subtotal { get; set; }

        public decimal Discount { get; set; }

        public decimal Tax { get; set; }

        public decimal Shipping { get; set; }

        public decimal Total { get; set; }

        public string TransactionReference { get; set; } = string.Empty;
    }

}
