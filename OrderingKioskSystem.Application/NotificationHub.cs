using Microsoft.AspNetCore.SignalR;

namespace OrderingKioskSystemManagement.Api.Configuration
{
    public class NotificationHub : Hub
    {
        public async Task NotifyStaff(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }
    }
}
