using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrderingKioskSystem.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.Order
{
    public class PendingOrderCleanupService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<PendingOrderCleanupService> _logger;

        public PendingOrderCleanupService(IServiceScopeFactory serviceScopeFactory, ILogger<PendingOrderCleanupService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await CleanUpPendingOrdersAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while cleaning up pending orders.");
                }

                // Wait for 1 minutes before checking again
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private async Task CleanUpPendingOrdersAsync()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var expirationTime = DateTime.UtcNow.AddHours(7).AddMinutes(-2);

                var pendingOrders = context.Orders
                    .Where(o => o.Status == "Pending" && o.NgayTao < expirationTime)
                    .ToList();

                if (pendingOrders.Any())
                {
                    foreach (var order in pendingOrders)
                    {
                        order.NgayXoa = DateTime.UtcNow.AddHours(7);
                    }
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
