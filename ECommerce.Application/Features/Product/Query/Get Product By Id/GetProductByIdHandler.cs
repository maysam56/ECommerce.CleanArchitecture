using ECommerce.Application.Interfaces.IRepository.ProductRepository;
using ECommerece.Domain.Entities;
using MediatR;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product?>
{
    private readonly IProductReadRepository _productReadRepository;

    public GetProductByIdHandler(IProductReadRepository productReadRepository)
    {
        _productReadRepository = productReadRepository;
    }
    public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return await _productReadRepository.GetByIdAsync(request.id , cancellationToken);

    }
}
