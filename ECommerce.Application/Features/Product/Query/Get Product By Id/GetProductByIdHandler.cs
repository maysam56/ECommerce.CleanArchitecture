using ECommerce.Application.Interfaces.IRepository.ProductRepository;
using ECommerece.Domain.Entities;
using MediatR;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product?>
{
    private readonly IProductReadRepository _productRepository;

    public GetProductByIdHandler(IProductReadRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return await _productRepository.GetByIdAsync(request.id , cancellationToken);

    }
}
