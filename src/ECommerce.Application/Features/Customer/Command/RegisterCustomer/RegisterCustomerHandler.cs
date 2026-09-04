using ECommerce.Application.Interfaces.IRepository.CustomerRepository;
using ECommerece.Domain.Entities;
using MediatR;

public class RegisterCustomerHandler : IRequestHandler<RegisterCustomerCommand, Customer>
{
    private readonly ICustomerWriteRepository _customerWriteRepository;
    private readonly ICustomerReadRepository _customerReadRepository;

    public RegisterCustomerHandler(ICustomerWriteRepository customerWriteRepository , ICustomerReadRepository customerReadRepository)
    {
        _customerWriteRepository = customerWriteRepository;
        _customerReadRepository = customerReadRepository;
    }
    public async Task<Customer> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;

        var emailExists = await _customerReadRepository.checkEmailExsist(dto.Email , cancellationToken);

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

        await _customerWriteRepository.AddAsync(customer, cancellationToken);
        await _customerWriteRepository.SaveChangesAsync(cancellationToken);
        return customer;

    }
}

