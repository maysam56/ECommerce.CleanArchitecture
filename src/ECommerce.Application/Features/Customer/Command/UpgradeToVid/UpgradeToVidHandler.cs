using ECommerce.Application.Abstractions.Persistence;
using ECommerce.Application.Interfaces.IRepository.CustomerRepository;
using ECommerece.Domain.Enum;
using MediatR;
public class UpgradeToVidHandler : IRequestHandler<UpgradeToVidCommand>
{
    private readonly ICustomerReadRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpgradeToVidHandler(
        ICustomerReadRepository customerRepository,
      IUnitOfWork  unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }
 

    async Task IRequestHandler<UpgradeToVidCommand>.Handle(UpgradeToVidCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId , cancellationToken);

        if (customer == null)
            throw new KeyNotFoundException($"Customer with ID {request.CustomerId} not found.");

        var totalSpent = customer.Orders
                          .Where(o => o.Status == OrderStatus.Paid)
                          .Sum(o => o.TotalAmount);

        if (totalSpent < 500m)
        {
            throw new InvalidOperationException(
           $"Customer does not qualify for VIP. " +
           $"Total spend {totalSpent:C} is less than required $500.00.");
        }
        customer.IsVip = true;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}   
