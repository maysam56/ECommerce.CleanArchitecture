using ECommerce.Application.Interfaces.IRepository.OrderRepository;
using ECommerece.Domain.Entities;
using MediatR;
public class GetOrdersByCustomerIdHandler : IRequestHandler<GetOrdersByCustomerIdQuery, IReadOnlyList<Order>>
{
    private readonly IOrderReadRepository _orderReadRepository;

    public GetOrdersByCustomerIdHandler(IOrderReadRepository orderReadRepository)
    {
        _orderReadRepository = orderReadRepository;
    }
    public async Task<IReadOnlyList<Order>> Handle(GetOrdersByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        return await _orderReadRepository.GetByCustomerIdAsync(request.id, cancellationToken);
    }
}