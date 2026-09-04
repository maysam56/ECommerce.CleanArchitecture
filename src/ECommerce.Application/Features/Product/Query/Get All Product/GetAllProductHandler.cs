using ECommerce.Application.Interfaces.IRepository.ProductRepository;
using ECommerece.Domain.Entities;
using MediatR;

public class GetAllProductHandler : IRequestHandler<GetAllProductQuery, IReadOnlyList<Product>>
{
    private readonly IProductReadRepository _productReadRepository;

    public GetAllProductHandler(IProductReadRepository productReadRepository)
    {
        _productReadRepository = productReadRepository;
    }

    public async Task<IReadOnlyList<Product>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        return await _productReadRepository.GetAllAsync(cancellationToken);

    }
}