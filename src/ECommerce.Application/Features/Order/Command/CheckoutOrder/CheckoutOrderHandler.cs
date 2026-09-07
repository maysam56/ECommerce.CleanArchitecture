using ECommerce.Application.Abstractions.Persistence;
using ECommerce.Application.Features.Order.DTOs;
using ECommerce.Application.Interfaces.IRepository.CouponRepository;
using ECommerce.Application.Interfaces.IRepository.CustomerRepository;
using ECommerce.Application.Interfaces.IRepository.OrderRepository;
using ECommerce.Application.Interfaces.IRepository.PaymentRepository;
using ECommerce.Application.Interfaces.IRepository.ProductRepository;
using ECommerece.Domain.Entities;
using ECommerece.Domain.Enum;
using MediatR;
public class CheckoutOrderHandler : IRequestHandler<CheckoutOrderCommand, CheckoutResponseDto>
{
    private readonly IOrderWriteRepository _orderWriteRepository;
    private readonly ICustomerReadRepository _customerReadRepository;
    private readonly IPaymentWriteRepository _paymentWriteRepository;
    private readonly ICouponReadRepository _couponReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductReadRepository _productReadRepository;

    public CheckoutOrderHandler(
        IOrderWriteRepository orderWriteRepository,
        ICustomerReadRepository customerReadRepository,
        IPaymentWriteRepository paymentWriteRepository,
        IProductReadRepository productReadRepository,
        ICouponReadRepository couponReadRepository ,
         IProductWriteRepository productWriteRepository ,
         IUnitOfWork unitOfWork


        )
    {
        _orderWriteRepository = orderWriteRepository;
        _customerReadRepository = customerReadRepository;
        _paymentWriteRepository = paymentWriteRepository;
        _couponReadRepository = couponReadRepository;
        _productWriteRepository = productWriteRepository;
        _unitOfWork = unitOfWork;
        _productReadRepository = productReadRepository;
    }
    public async Task<CheckoutResponseDto> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {

      
        var customer = await _customerReadRepository.GetByIdAsync(request.dto.CustomerId , cancellationToken);

        if (customer == null)
        {
            throw new KeyNotFoundException(
                $"Customer with ID {request.dto.CustomerId} not found.");
        }

        decimal subtotal = 0m;

        var orderItems = new List<OrderItem>();

        foreach (var itemDto in request.dto.Items)
        {
            if (itemDto.Quantity <= 0)
            {
                throw new ArgumentException(
                    "Product quantity must be at least 1.");
            }

            var product = await _productReadRepository
                .GetByIdAsync(itemDto.ProductId, cancellationToken);

            if (product == null)
            {
                throw new KeyNotFoundException(
                    $"Product with ID {itemDto.ProductId} not found.");
            }

            if (product.StockQuantity < itemDto.Quantity)
            {
                throw new InvalidOperationException(
                    $"Insufficient stock for product '{product.Name}'. " +
                    $"Available: {product.StockQuantity}, " +
                    $"Requested: {itemDto.Quantity}");
            }

            subtotal += product.Price * itemDto.Quantity;

            orderItems.Add(new OrderItem
            {
                ProductId = product.Id,
                Quantity = itemDto.Quantity,
                UnitPrice = product.Price
            });

            product.SetStockQuantity(
                product.StockQuantity - itemDto.Quantity);

            await _productWriteRepository.UpdateProductAsync(product);
        }

        decimal discount = 0m;

        if (customer.IsVip)
        {
            discount += Math.Round(subtotal * 0.15m, 2);
        }

        if (!string.IsNullOrWhiteSpace(request.dto.CouponCode))
        {
            var coupon = await _couponReadRepository
                .GetByCodeAsync(request.dto.CouponCode, cancellationToken);

            if (coupon == null || !coupon.IsActive)
            {
                throw new InvalidOperationException(
                    $"Invalid or inactive coupon code '{request.dto.CouponCode}'.");
            }

            discount += Math.Round(
                subtotal * (coupon.DiscountPercentage / 100m), 2);
        }

        if (discount > subtotal)
        {
            discount = subtotal;
        }

        var netAmount = subtotal - discount;

        var tax = Math.Round(netAmount * 0.14m, 2);

        var shipping = netAmount >= 1000m
            ? 0m
            : 75m;

        var finalTotal = netAmount + tax + shipping;

        if (finalTotal > 50000m)
        {
            throw new InvalidOperationException(
                "Payment processing failed. Amount exceeds limit.");
        }

        var txRef =
            $"TX-LEGACY-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

        var order = new Order
        {
            CustomerId = customer.Id,
            CreatedAt = DateTime.UtcNow,
            Status = OrderStatus.Paid,
            Subtotal = subtotal,
            DiscountAmount = discount,
            TaxAmount = tax,
            ShippingFee = shipping,
            TotalAmount = finalTotal,
            Items = orderItems
        };

        var payment = new Payment
        {
            Order = order,
            Amount = finalTotal,
            PaymentDate = DateTime.UtcNow,
            TransactionReference = txRef,
            IsSuccess = true
        };

        await _orderWriteRepository.AddAsync(order, cancellationToken);
        await _paymentWriteRepository.AddAsync(payment, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CheckoutResponseDto
        {
            OrderId = order.Id,
            Status = order.Status.ToString(),
            Subtotal = order.Subtotal,
            Discount = order.DiscountAmount,
            Tax = order.TaxAmount,
            Shipping = order.ShippingFee,
            Total = order.TotalAmount,
            TransactionReference = txRef
        };
    }



}

    
