using ECommerce.Application.Abstractions.Persistence;
using ECommerce.Application.Interfaces.IRepository.CartRepository;
using MediatR;

namespace ECommerce.Application.Features.Carts.Command.RemoveExpiredCartItems;

public class RemoveExpiredCartItemsCommandHandler  : IRequestHandler<RemoveExpiredCartItemsCommand>
  
{
    private readonly ICartReadRepository _cartRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveExpiredCartItemsCommandHandler(
        ICartReadRepository cartRepository ,
        IUnitOfWork unitOfWork )
       
    {
        _cartRepository = cartRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        RemoveExpiredCartItemsCommand request,
        CancellationToken cancellationToken)
    {
        var expirationDate =
            DateTime.UtcNow.AddDays(-7);

        var expiredItems =
            await _cartRepository.GetExpiredItemsAsync(
                expirationDate,
                cancellationToken);

        foreach (var item in expiredItems)
        {
            item.Cart.RemoveItem(item);
        }

        await _unitOfWork.SaveChangesAsync( cancellationToken);
           
    }
}