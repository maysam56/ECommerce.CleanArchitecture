using ECommerce.Application.Interfaces.IRepository.OrderRepository;
using ECommerce.Application.Interfaces.IRepository.ProductRepository;
using ECommerce.DAL.Entities;
using ECommerece.Domain.Entities;
using MediatR;

public class CancelOrderHandler : IRequestHandler<CancelOrderCommand>
{
    private readonly IOrderReadRepository   _orderRepository;
    private readonly IProductReadRepository _productRepository;

    public CancelOrderHandler( IOrderReadRepository orderRepository , IProductReadRepository productRepository)
    {
      _orderRepository = orderRepository;
        _productRepository = productRepository;
    }
    public async Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {

        var order = await _orderRepository.GetForCancelAsync(request.id, cancellationToken);

        if (order == null) throw new KeyNotFoundException("Order not found");

        if (order.Status == OrderStatus.Cancelled)
            throw new InvalidOperationException("Order is already cancelled");


        if (order.Status == OrderStatus.Paid)
        {
            foreach (var item in order.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId, cancellationToken);
                if (product != null)
                {
                    product.SetStockQuantity(product.StockQuantity + item.Quantity);
                }
            }
        }

        order.Status = OrderStatus.Cancelled;
        await _orderRepository.SaveChangesAsync(cancellationToken);

    }
}