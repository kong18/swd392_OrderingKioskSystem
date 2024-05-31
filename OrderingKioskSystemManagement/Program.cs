
using Microsoft.AspNetCore.Hosting;
using OrderingKioskSystem.Application.User.SendEmail;
using Serilog;
using Serilog.Events;

namespace OrderingKioskSystemManagement.Api;
public static class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateBootstrapLogger();

        try
        {
            Log.Information("Starting web host");
            CreateHostBuilder(args).Build().Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog((context, services, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("Logs/logs.txt", rollingInterval: RollingInterval.Day))

            .ConfigureAppConfiguration((context, config) =>
            {
                // Build the configuration
                var builtConfig = config.Build();

                // Load the EmailSettings and set the singleton instance
                EmailSettingModel.Instance = builtConfig.GetSection("EmailSettings").Get<EmailSettingModel>();
            })

            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}