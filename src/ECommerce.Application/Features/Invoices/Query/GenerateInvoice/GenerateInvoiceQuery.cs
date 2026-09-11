using MediatR;

namespace ECommerce.Application.Features.Invoices.Query.GenerateInvoice;

public record GenerateInvoiceQuery(int OrderId) : IRequest<byte[]>;