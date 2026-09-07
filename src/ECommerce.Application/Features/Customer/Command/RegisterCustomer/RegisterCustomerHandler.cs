using ECommerce.Application.Abstractions.Persistence;
using ECommerce.Application.Interfaces.IRepository.CustomerRepository;
using ECommerece.Domain.Entities;
using MediatR;

public class RegisterCustomerHandler : IRequestHandler<RegisterCustomerCommand, Customer>
{
    private readonly ICustomerWriteRepository _customerWriteRepository;
    private readonly ICustomerReadRepository _customerReadRepository;
    private readonly IUnitOfWork _unitOfWork;
    public RegisterCustomerHandler(
        ICustomerWriteRepository customerWriteRepository , 
        ICustomerReadRepository customerReadRepository ,
        IUnitOfWork unitOfWork)
    {
        _customerWriteRepository = customerWriteRepository;
        _customerReadRepository = customerReadRepository;
        _unitOfWork = unitOfWork;
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
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return customer;

    }
}

