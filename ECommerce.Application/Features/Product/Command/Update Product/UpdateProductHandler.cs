using ECommerce.Application.Interfaces.IRepository.ProductRepository;
using MediatR;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;

    public UpdateProductHandler( IProductReadRepository productReadRepository ,
        IProductWriteRepository productWriteRepository  )
       
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
    }
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var existing = await _productReadRepository.GetByIdAsync(request.id, cancellationToken);

        if (existing == null) throw new KeyNotFoundException($"Product with ID {request.id} not found.");

        existing.Name = request.product.Name;
        existing.SKU = request.product.SKU;
        existing.SetPrice(request.product.Price);
        existing.SetStockQuantity(request.product.StockQuantity);

        await _productWriteRepository.UpdateProductAsync(existing);
        await _productWriteRepository.SaveChangesAsync(cancellationToken);
    }
}