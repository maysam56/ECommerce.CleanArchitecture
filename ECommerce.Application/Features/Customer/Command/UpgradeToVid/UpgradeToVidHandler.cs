using ECommerce.Application.Interfaces.IRepository.CustomerRepository;
using ECommerce.DAL.Entities;
using MediatR;
public class UpgradeToVidHandler : IRequestHandler<UpgradeToVidCommand>
{
    private readonly ICustomerReadRepository _customerRepository;
    public UpgradeToVidHandler(ICustomerReadRepository customerRepository)
    {
        _customerRepository = customerRepository;
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
        await _customerRepository.SaveChangesAsync(cancellationToken);
    }
}   
