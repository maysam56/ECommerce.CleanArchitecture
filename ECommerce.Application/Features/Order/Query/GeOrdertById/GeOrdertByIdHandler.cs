using ECommerce.Application.Interfaces.IRepository.OrderRepository;
using ECommerece.Domain.Entities;
using MediatR;
public class GeOrdertByIdHandler : IRequestHandler<GeOrdertByIdQuery, Order?>
{
    private readonly IOrderReadRepository _orderRepository;

    public GeOrdertByIdHandler(IOrderReadRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Order?> Handle(GeOrdertByIdQuery request, CancellationToken cancellationToken)
    {
        return await _orderRepository.GetByIdAsync(request.id, cancellationToken);
    }
}
