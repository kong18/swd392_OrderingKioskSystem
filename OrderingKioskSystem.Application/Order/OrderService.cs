using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using OrderingKioskSystemManagement.Api.Configuration;

namespace OrderingKioskSystem.Application.Order
{
    public class OrderService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public OrderService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyNewOrder(string orderId)
        {
            // Notify staff about the new order
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", $"A new order has been placed with Order ID: {orderId}");
        }
    }
}
