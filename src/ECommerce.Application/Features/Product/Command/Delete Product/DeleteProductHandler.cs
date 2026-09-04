using ECommerce.Application.Interfaces.IRepository.ProductRepository;
using MediatR;
public class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;

    public DeleteProductHandler(IProductReadRepository productReadRepository ,
        IProductWriteRepository productWriteRepository
        ) {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productReadRepository.GetByIdAsync(request.id , cancellationToken);
        if (product == null)
        {
            throw new KeyNotFoundException($"Product with id {request.id} not found.");
        }
        await _productWriteRepository.DeleteProductAsync(product);
    }
}
