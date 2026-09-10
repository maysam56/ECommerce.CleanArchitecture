using ECommerce.Application.Abstractions.Persistence;
using ECommerce.Application.Interfaces.IRepository.CartRepository;
using ECommerce.Application.Interfaces.IRepository.CouponRepository;
using ECommerce.Application.Interfaces.IRepository.CustomerRepository;
using ECommerce.Application.Interfaces.IRepository.OrderRepository;
using ECommerce.Application.Interfaces.IRepository.PaymentRepository;
using ECommerce.Application.Interfaces.IRepository.ProductRepository;
using ECommerce.Application.Interfaces.IServices;
using ECommerce.Application.Settings;
using ECommerce.Infrastructure.Data.Context;
using ECommerce.Infrastructure.Email;
using ECommerce.Infrastructure.Repositories.CartRepository;
using ECommerce.Infrastructure.Repositories.CouponRepository;
using ECommerce.Infrastructure.Repositories.CustomerRepository;
using ECommerce.Infrastructure.Repositories.OrderRepository;
using ECommerce.Infrastructure.Repositories.PaymentRepository;
using ECommerce.Infrastructure.Repositories.Persistence;
using ECommerce.Infrastructure.Repositories.ProductRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            // Read DbContext

            services.AddDbContext<AppReadDbContext>(options =>
            options.UseSqlServer(
            configuration.GetConnectionString("DefaultConnection")));

            // Write DbContext

            services.AddDbContext<AppReadDbContext>(options =>
          options.UseSqlServer(
          configuration.GetConnectionString("DefaultConnection")));

            // Order
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

            // Customer
            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();

            // Product
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            // Payment
            services.AddScoped<IPaymentReadRepository, PaymentReadRepository>();
            services.AddScoped<IPaymentWriteRepository, PaymentWriteRepository>();

            // Coupon
            services.AddScoped<ICouponWriteRepository, CouponWriteRepository>();
            services.AddScoped<ICouponReadRepository, CouponReadRepository>();

            // Cart
            services.AddScoped<ICartReadRepository, CartReadRepository>();
            services.AddScoped<ICartWriteRepository, CartWriteRepository>();

            // Email
            services.AddScoped<IEmailService, EmailService>();

            // Unit Of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Email Settings
          
            services.Configure<EmailSettings>(configuration.GetSection("Email"));
                    
            return services;
        }
    }
}
