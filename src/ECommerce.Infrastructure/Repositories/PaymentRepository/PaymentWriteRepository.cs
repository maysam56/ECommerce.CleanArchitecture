using ECommerce.Application.Interfaces.IRepository.PaymentRepository;
using ECommerce.Infrastructure.Data.Context;
using ECommerece.Domain.Entities;

namespace ECommerce.Infrastructure.Repositories.PaymentRepository
{

    public class PaymentWriteRepository :IPaymentWriteRepository
    {
        private readonly AppWriteDbContext _context;
        public PaymentWriteRepository(AppWriteDbContext context)
        {

            _context = context;
        }

        public async Task AddAsync(Payment payment, CancellationToken cancellationToken)
        {

            await _context.Payments.AddAsync(payment, cancellationToken);
        }
        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {

            await _context.SaveChangesAsync(cancellationToken);
        }

    }
}
