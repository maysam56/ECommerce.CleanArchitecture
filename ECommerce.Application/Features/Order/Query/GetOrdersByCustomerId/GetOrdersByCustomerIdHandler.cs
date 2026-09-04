using ECommerce.Application.Interfaces.IRepository.OrderRepository;
using ECommerece.Domain.Entities;
using MediatR;
public class GetOrdersByCustomerIdHandler : IRequestHandler<GetOrdersByCustomerIdQuery, IReadOnlyList<Order>>
{
    private readonly IOrderReadRepository _orderRepository;

    public GetOrdersByCustomerIdHandler(IOrderReadRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<IReadOnlyList<Order>> Handle(GetOrdersByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        return await _orderRepository.GetByCustomerIdAsync(request.id, cancellationToken);
    }
}