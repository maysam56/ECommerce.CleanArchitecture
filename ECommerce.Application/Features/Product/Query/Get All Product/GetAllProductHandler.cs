using ECommerce.Application.Interfaces.IRepository.ProductRepository;
using ECommerece.Domain.Entities;
using MediatR;

public class GetAllProductHandler : IRequestHandler<GetAllProductQuery, IReadOnlyList<Product>>
{
    private readonly IProductReadRepository _productRepository;

    public GetAllProductHandler(IProductReadRepository productRepository)
    {
       _productRepository = productRepository;
    }

    public async Task<IReadOnlyList<Product>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        return await _productRepository.GetAllAsync(cancellationToken);

    }
}