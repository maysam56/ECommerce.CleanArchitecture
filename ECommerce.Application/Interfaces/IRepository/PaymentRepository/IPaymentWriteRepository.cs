using ECommerece.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Interfaces.IRepository.PaymentRepository
{
    public interface IPaymentWriteRepository
    {
        Task AddAsync(Payment payment, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }

}
