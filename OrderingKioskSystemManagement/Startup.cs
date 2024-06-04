using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using OrderingKioskSystemManagement.Api.Configuration;
using OrderingKioskSystemManagement.Api.Filters;
using Serilog;
using OrderingKioskSystem.Application;
using OrderingKioskSystem.Infrastructure;
using OrderingKioskSystem.Application.Order;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using OrderingKioskSystemManagement.Application;
using OrderingKioskSystem.Application.FileUpload;
using System;
using System.IO;

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
            services.AddControllers(
                opt =>
                {
                    opt.Filters.Add<ExceptionFilter>();
                });
            services.AddSignalR();
            services.AddScoped<OrderService>();
            services.AddApplication(Configuration);
            services.ConfigureApplicationSecurity(Configuration);
            services.ConfigureProblemDetails();

            services.ConfigureApiVersioning();
            services.AddInfrastructure(Configuration);
            services.ConfigureSwagger(Configuration);
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .WithOrigins("https://localhost:7182")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                );
            });
            
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            // Configure Firebase
            var firebaseConfigPath = @"D:\Config\oderingkiosksystem-firebase-adminsdk-8l6s1-1d9328443e.json"; // Use the actual path to your JSON file
            if (!File.Exists(firebaseConfigPath))
            {
                throw new FileNotFoundException($"Firebase configuration file not found at {firebaseConfigPath}");
            }

            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(firebaseConfigPath)
            });

            services.AddSingleton(new FirebaseConfig { Type = "service_account" });
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
            app.UseAuthentication(); // Ensure this is before UseAuthorization
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
