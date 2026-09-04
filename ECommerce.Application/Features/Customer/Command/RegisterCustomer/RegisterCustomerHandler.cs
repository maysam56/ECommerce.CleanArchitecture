using ECommerce.Application.Interfaces.IRepository.CustomerRepository;
using ECommerece.Domain.Entities;
using MediatR;

public class RegisterCustomerHandler : IRequestHandler<RegisterCustomerCommand, Customer>
{
    private readonly ICustomerReadRepository _customerRepository;

    public RegisterCustomerHandler(ICustomerReadRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    public async Task<Customer> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;

        var emailExists = await _customerRepository.checkEmailExsist(dto.Email , cancellationToken);

        if (emailExists)
        {
            throw new InvalidOperationException("Email is already registered.");
        }

        var customer = new Customer
        {
            FullName = dto.FullName,
            Email = dto.Email,
            IsVip = dto.IsVip
        };

        await _customerRepository.AddAsync(customer, cancellationToken);
        await _customerRepository.SaveChangesAsync(cancellationToken);
        return customer;

    }
}

