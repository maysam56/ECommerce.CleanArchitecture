using ECommerce.Application.Abstractions.Persistence;
using ECommerce.Application.Interfaces.IRepository.CartRepository;
using ECommerce.Application.Interfaces.IServices;
using MediatR;

namespace ECommerce.Application.Features.Cart.Commands.SendCartReminderEmails;

public class SendCartReminderEmailsHandler
    : IRequestHandler<SendCartReminderEmailsCommand>
{
    private readonly IEmailService _emailService;
    private readonly ICartReadRepository _cartReadRepository;
    private readonly ICartWriteRepository _cartWriteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SendCartReminderEmailsHandler(
        IEmailService emailService,
        ICartReadRepository cartReadRepository,
        ICartWriteRepository cartWriteRepository,
        IUnitOfWork unitOfWork)
    {
        _emailService = emailService;
        _cartReadRepository = cartReadRepository;
        _cartWriteRepository = cartWriteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        SendCartReminderEmailsCommand request,
        CancellationToken cancellationToken)
    {
        var items = await _cartReadRepository
            .GetItemsForReminderAsync(cancellationToken);

        foreach (var item in items)
        {
            var customerEmail = item.Cart.customer.Email;
            var customerName = item.Cart.customer.FullName;
            var productName = item.Product.Name;

            var subject = "You left something in your cart 🛒";

            var body = $"""
                Hi {customerName},

                You still have "{productName}" in your shopping cart.

                Don't forget to complete your purchase.

                Thank you for shopping with us!
                """;

            await _emailService.SendEmailAsync(
                customerEmail,
                subject,
                body);

            _cartWriteRepository.MarkReminderAsSent(item);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}