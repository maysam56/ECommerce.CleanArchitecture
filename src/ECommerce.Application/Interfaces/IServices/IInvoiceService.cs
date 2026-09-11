
using ECommerce.Application.Features.Invoices.DTO;

namespace ECommerce.Application.Interfaces.IServices
{
    public interface IInvoiceService
    {
        Task<byte[]> GenerateInvoiceAsync(InvoiceDto invoice);
    }
}
