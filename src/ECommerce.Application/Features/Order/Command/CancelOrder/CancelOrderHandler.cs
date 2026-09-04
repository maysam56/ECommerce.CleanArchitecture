using ECommerce.Application.Interfaces.IRepository.OrderRepository;
using ECommerce.Application.Interfaces.IRepository.ProductRepository;
using ECommerece.Domain.Enum;
using MediatR;

public class CancelOrderHandler : IRequestHandler<CancelOrderCommand>
{
    private readonly IOrderReadRepository _orderReadRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IOrderWriteRepository _orderWriteRepository;

    public CancelOrderHandler( IOrderReadRepository orderReadRepository ,
        IProductReadRepository productReadRepository ,
        IOrderWriteRepository orderWriteRepository
        )
    {
        _orderReadRepository = orderReadRepository;
        _productReadRepository = productReadRepository;
        _orderWriteRepository = orderWriteRepository;
    }
    public async Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {

        var order = await _orderReadRepository.GetForCancelAsync(request.id, cancellationToken);

        if (order == null) throw new KeyNotFoundException("Order not found");

        if (order.Status == OrderStatus.Cancelled)
            throw new InvalidOperationException("Order is already cancelled");


        if (order.Status == OrderStatus.Paid)
        {
            foreach (var item in order.Items)
            {
                var product = await _productReadRepository.GetByIdAsync(item.ProductId, cancellationToken);
                if (product != null)
                {
                    product.SetStockQuantity(product.StockQuantity + item.Quantity);
                }
            }
        }

        order.Status = OrderStatus.Cancelled;
        await _orderWriteRepository.SaveChangesAsync(cancellationToken);

    }
}