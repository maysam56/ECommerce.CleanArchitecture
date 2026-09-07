using ECommerce.Application.Abstractions.Persistence;
using ECommerce.Application.Interfaces.IRepository.ProductRepository;
using ECommerece.Domain.Entities;
using MediatR;

public class AddProductHandler : IRequestHandler<AddProductCommand, Product>
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddProductHandler(IProductReadRepository productReadRepository ,
        IProductWriteRepository productWriteRepository ,
        IUnitOfWork unitOfWork){
        
      
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
        _unitOfWork = unitOfWork;
    }
    public async  Task<Product> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var skuExists = await _productReadRepository.ExistsBySkuAsync(request.productDto.SKU , cancellationToken);

        if (skuExists)
            throw new InvalidOperationException($"Product with SKU '{request.productDto.SKU}' already exists.");

        var product = new Product
        {
            Name = request.productDto.Name,
            SKU = request.productDto.SKU.ToUpper()
        };

        product.SetPrice(request.productDto.Price);
        product.SetStockQuantity(request.productDto.StockQuantity);

        await _productWriteRepository.AddProductAsync(product , cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return product;
    }
}