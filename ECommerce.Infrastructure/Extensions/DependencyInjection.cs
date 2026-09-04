using ECommerce.Application.Interfaces.IRepository.CouponRepository;
using ECommerce.Application.Interfaces.IRepository.CustomerRepository;
using ECommerce.Application.Interfaces.IRepository.OrderRepository;
using ECommerce.Application.Interfaces.IRepository.PaymentRepository;
using ECommerce.Application.Interfaces.IRepository.ProductRepository;
using ECommerce.Application.Interfaces.IServices;
using ECommerce.Application.Services;
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

            services.AddScoped<IOrderReadRepository, OrderWriteRepository>();
            services.AddScoped<ICustomerReadRepository, CustomerWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IOrdersServices, OrdersServices>();
            services.AddScoped<IProductsServices, ProductService>();
            services.AddScoped<IPaymentReadRepository, PaymentReadRepository>();
            services.AddScoped<ICouponRepository, CouponReadRepository>();
          
            return services;
        }
    }
}
