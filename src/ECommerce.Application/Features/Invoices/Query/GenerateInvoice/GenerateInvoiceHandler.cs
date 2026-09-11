using ECommerce.Application.Features.Invoices.Mapping;
using ECommerce.Application.Interfaces.IRepository.OrderRepository;
using ECommerce.Application.Interfaces.IServices;
using MediatR;

namespace ECommerce.Application.Features.Invoices.Query.GenerateInvoice;

public class GenerateInvoiceHandler: IRequestHandler<GenerateInvoiceQuery, byte[]>
{
    private readonly IOrderReadRepository _orderReadRepository;
    private readonly IInvoiceService _invoiceService;

    public GenerateInvoiceHandler(
        IOrderReadRepository orderReadRepository,
        IInvoiceService invoiceService)
    {
        _orderReadRepository = orderReadRepository;
        _invoiceService = invoiceService;
    }

    public async Task<byte[]> Handle(
        GenerateInvoiceQuery request,
        CancellationToken cancellationToken)
    {
        var order = await _orderReadRepository
            .GetOrderForInvoiceAsync(
                request.OrderId,
                cancellationToken);

        if (order is null)
        {
            throw new KeyNotFoundException("Order not found.");
        }

        var invoice = order.ToInvoiceDto();

        var pdf = await _invoiceService
            .GenerateInvoiceAsync(invoice);

        return pdf;
    }
}