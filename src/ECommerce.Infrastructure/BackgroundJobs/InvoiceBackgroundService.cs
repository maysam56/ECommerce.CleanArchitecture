using ECommerce.Application.Abstractions.Persistence;
using ECommerce.Application.Features.Invoices.Query.GenerateInvoice;
using ECommerce.Application.Interfaces.IRepository.OrderRepository;
using ECommerce.Application.Interfaces.IServices;
using ECommerce.Application.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ECommerce.Infrastructure.BackgroundJobs;

public class InvoiceBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public InvoiceBackgroundService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();

            var orderReadRepository =
                scope.ServiceProvider.GetRequiredService<IOrderReadRepository>();

            var emailService =
                scope.ServiceProvider.GetRequiredService<IEmailService>();

            var unitOfWork =
                scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var mediator =
                scope.ServiceProvider.GetRequiredService<IMediator>();

            var orders = await orderReadRepository
                .GetOrdersForInvoiceAsync(stoppingToken);

            foreach (var order in orders)
            {
                var pdf = await mediator.Send(
                    new GenerateInvoiceQuery(order.Id),
                    stoppingToken);

                var attachment = new EmailAttachment
                {
                    FileName = $"Invoice-{order.Id}.pdf",
                    Content = pdf,
                    ContentType = "application/pdf"
                };

                var customerName = order.Customer!.FullName;
                var orderId = order.Id;
                var orderDate = order.CreatedAt.ToString("dd/MM/yyyy");
                var totalAmount = order.TotalAmount.ToString("0.00");

                var subject = $"Your Invoice - Order #{orderId}";

                var body = $"""
                    <h2>Thank you for your order!</h2>

                    <p>Dear {customerName},</p>

                    <p>
                        Thank you for shopping with us.
                        Please find your invoice attached to this email.
                    </p>

                    <p>
                        <strong>Order Number:</strong> #{orderId}<br>
                        <strong>Order Date:</strong> {orderDate}<br>
                        <strong>Total Amount:</strong> {totalAmount}
                    </p>

                    <p>
                        We appreciate your business and hope to see you again soon.
                    </p>

                    <p>Best regards,<br>E-Commerce Team</p>
                    """;

                await emailService.SendEmailAsync(
                    order.Customer.Email,
                    subject,
                    body,
                    [attachment]);

                order.InvoiceSentAt = DateTime.UtcNow;

                await unitOfWork.SaveChangesAsync(stoppingToken);
            }

            await Task.Delay(
                TimeSpan.FromMinutes(1),
                stoppingToken);
        }
    }
}