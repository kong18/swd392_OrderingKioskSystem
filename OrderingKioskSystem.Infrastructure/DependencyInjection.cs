using OrderingKioskSystem.Domain.Common.Interfaces;
using OrderingKioskSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderingKioskSystem.Domain.Repositories;
using OrderingKioskSystem.Infrastructure.Repositories;
namespace OrderingKioskSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("server"),
                b =>
                {
                    b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                });
            options.UseLazyLoadingProxies();
        });
        
        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IProductMenuRepository, ProductMenuRepository>();
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<IMenuRepository, MenuRepository>();
        services.AddTransient<IBusinessRepository, BusinessRepository>();
        services.AddTransient<IKioskRepository, KioskRepository>();
        services.AddTransient<IShipperRepository, ShipperRepository>();
        services.AddTransient<IPaymentRepository, PaymentRepository>();
        services.AddTransient<IPaymentGatewayRepository, PaymentGatewayRepository>();
        services.AddTransient<IUserRepository, UserRepository>();


        return services;
    }
}