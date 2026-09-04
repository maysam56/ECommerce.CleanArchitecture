using ECommerce.Application.Interfaces.IRepository.ProductRepository;
using MediatR;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductReadRepository _productRepository;

    public UpdateProductHandler(IProductReadRepository productRepository)
    {
       _productRepository = productRepository;
    }
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var existing = await _productRepository.GetByIdAsync(request.id, cancellationToken);

        if (existing == null) throw new KeyNotFoundException($"Product with ID {request.id} not found.");

        existing.Name = request.product.Name;
        existing.SKU = request.product.SKU;
        existing.SetPrice(request.product.Price);
        existing.SetStockQuantity(request.product.StockQuantity);

        await _productRepository.UpdateProductAsync(existing);
        await _productRepository.SaveChangesAsync(cancellationToken);
    }
}