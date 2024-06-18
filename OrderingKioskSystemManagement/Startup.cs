using OrderingKioskSystemManagement.Api.Configuration;
using OrderingKioskSystemManagement.Api.Filters;
using Serilog;
using OrderingKioskSystem.Application;
using OrderingKioskSystem.Infrastructure;
using OrderingKioskSystem.Application.Order;
using OrderingKioskSystemManagement.Application;
using OrderingKioskSystem.Application.FileUpload;
using System.IO;
using System.Text.Json;
using SWD.OrderingKioskSystem.Application.Order;

namespace OrderingKioskSystemManagement.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(opt => opt.Filters.Add(typeof(ExceptionFilter)))
                    .AddJsonOptions(opt => opt.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.KebabCaseLower);
            services.AddSignalR();
            services.AddScoped<OrderService>();
            services.AddApplication(Configuration);
            services.ConfigureApplicationSecurity(Configuration);
            services.ConfigureProblemDetails();
            services.ConfigureApiVersioning();
            services.AddInfrastructure(Configuration);
            services.ConfigureSwagger(Configuration);
            services.AddHostedService<PendingOrderCleanupService>();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .WithOrigins("https://localhost:7182")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
            });

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            // Additional logging
            var firebaseSection = Configuration.GetSection("FirebaseConfig");
            if (!firebaseSection.Exists())
            {
                throw new ArgumentNullException("FirebaseConfig section does not exist in configuration.");
            }

            var firebaseConfig = firebaseSection.Get<FirebaseConfig>();
            if (firebaseConfig == null)
            {
                throw new ArgumentNullException(nameof(firebaseConfig), "FirebaseConfig section is missing in configuration.");
            }

            services.AddSingleton(firebaseConfig);
            services.AddSingleton<FileUploadService>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseSerilogRequestLogging();
            app.UseExceptionHandler();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<OrderingKioskSystem.Application.NotificationHub>("/notificationHub");
            });

            app.UseSwashbuckle(Configuration);
        }
    }
}
