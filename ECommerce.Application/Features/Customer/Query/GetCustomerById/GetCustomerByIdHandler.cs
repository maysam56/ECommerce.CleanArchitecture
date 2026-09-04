using ECommerce.Application.Interfaces.IRepository.CustomerRepository;
using ECommerece.Domain.Entities;
using MediatR;
public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, Customer?>
{
    public readonly ICustomerReadRepository _customerRepository;
    public GetCustomerByIdHandler(ICustomerReadRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Customer?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        return await _customerRepository.GetByIdAsync(request.CustomerId , cancellationToken);
    }
}
