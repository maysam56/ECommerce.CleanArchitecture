using ECommerce.Application.Interfaces.IRepository.CustomerRepository;
using ECommerece.Domain.Entities;
using MediatR;
public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, Customer?>
{
    private readonly ICustomerReadRepository _customerReadRepository;

    public GetCustomerByIdHandler(ICustomerReadRepository customerReadRepository)
    {
        _customerReadRepository = customerReadRepository;
    }

    public async Task<Customer?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        return await _customerReadRepository.GetByIdAsync(request.CustomerId , cancellationToken);
    }
}
