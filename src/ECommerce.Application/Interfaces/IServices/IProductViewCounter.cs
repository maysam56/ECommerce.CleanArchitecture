namespace ECommerce.Application.Interfaces.IServices;

public interface IProductViewCounter
{
    Task IncrementAsync(
        int productId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<int>> GetViewedProductIdsAsync(
        CancellationToken cancellationToken = default);
    Task<long> TakeCountAsync(
       int productId,
       CancellationToken cancellationToken = default);
    Task RemoveViewedProductAsync(
     int productId,
     CancellationToken cancellationToken = default);
}