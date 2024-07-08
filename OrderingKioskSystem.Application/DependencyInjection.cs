using FluentValidation;
using OrderingKioskSystem.Application.Common.Behaviours;
using OrderingKioskSystem.Application.Common.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;
using OrderingKioskSystem.Application.FileUpload;
using SWD.OrderingKioskSystem.Domain.Repositories;
using SWD.OrderingKioskSystem.Application.VNPay;
using SWD.OrderingKioskSystem.Application.Payment;

namespace OrderingKioskSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), lifetime: ServiceLifetime.Transient);
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
                cfg.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
                cfg.AddOpenBehavior(typeof(AuthorizationBehaviour<,>));
                cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
                cfg.AddOpenBehavior(typeof(UnitOfWorkBehaviour<,>));
            });
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IValidatorProvider, ValidatorProvider>();
            services.AddTransient<FileUploadService>();
            services.AddScoped<IVnPayService, VnPayService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddMemoryCache();
            return services;
        }
    }
}