using ECommerce.Application.Interfaces.IRepository.CouponRepository;
using ECommerce.Application.Interfaces.IRepository.CustomerRepository;
using ECommerce.Application.Interfaces.IRepository.OrderRepository;
using ECommerce.Application.Interfaces.IRepository.PaymentRepository;
using ECommerce.Application.Interfaces.IRepository.ProductRepository;
using ECommerce.Infrastructure.Data.Context;
using ECommerce.Infrastructure.Repositories.CouponRepository;
using ECommerce.Infrastructure.Repositories.CustomerRepository;
using ECommerce.Infrastructure.Repositories.OrderRepository;
using ECommerce.Infrastructure.Repositories.PaymentRepository;
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
            services.AddDbContext<AppReadDbContext>(options =>
            options.UseSqlServer(
            configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();

            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            services.AddScoped<IPaymentReadRepository, PaymentReadRepository>();
            services.AddScoped<IPaymentWriteRepository, PaymentWriteRepository>();

            services.AddScoped<ICouponWriteRepository, CouponWriteRepository>();
            services.AddScoped<ICouponReadRepository, CouponReadRepository>();


            return services;
        }
    }
}
