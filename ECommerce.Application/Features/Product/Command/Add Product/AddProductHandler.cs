using ECommerce.Application.Interfaces.IRepository.ProductRepository;
using ECommerece.Domain.Entities;
using MediatR;

public class AddProductHandler : IRequestHandler<AddProductCommand, Product>
{
    private readonly IProductReadRepository _productRepository;

    public AddProductHandler(IProductReadRepository productRepository) {
       _productRepository = productRepository;
    }
    public async  Task<Product> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var skuExists = await _productRepository.ExistsBySkuAsync(request.productDto.SKU , cancellationToken);

        if (skuExists)
            throw new InvalidOperationException($"Product with SKU '{request.productDto.SKU}' already exists.");

        var product = new Product
        {
            Name = request.productDto.Name,
            SKU = request.productDto.SKU.ToUpper()
        };

        product.SetPrice(request.productDto.Price);
        product.SetStockQuantity(request.productDto.StockQuantity);

        await _productRepository.AddProductAsync(product , cancellationToken);
        await _productRepository.SaveChangesAsync(cancellationToken);
        return product;
    }
}