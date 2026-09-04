using ECommerce.Application.Interfaces.IRepository.ProductRepository;
using MediatR;
public class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductReadRepository _productRepository;

    public DeleteProductHandler(IProductReadRepository productRepository) {
        _productRepository = productRepository;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.id , cancellationToken);
        if (product == null)
        {
            throw new KeyNotFoundException($"Product with id {request.id} not found.");
        }
        await _productRepository.DeleteProductAsync(product);
    }
}
