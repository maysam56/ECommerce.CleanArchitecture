using ECommerce.Application.Interfaces.IRepository.OrderRepository;
using ECommerece.Domain.Entities;
using MediatR;
public class GeOrdertByIdHandler : IRequestHandler<GeOrdertByIdQuery, Order?>
{
    private readonly IOrderReadRepository _orderReadRepository;

    public GeOrdertByIdHandler(IOrderReadRepository orderReadRepository)
    {
        _orderReadRepository = orderReadRepository;
    }

    public async Task<Order?> Handle(GeOrdertByIdQuery request, CancellationToken cancellationToken)
    {
        return await _orderReadRepository.GetByIdAsync(request.id, cancellationToken);
    }
}
