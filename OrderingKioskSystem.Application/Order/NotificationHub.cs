using Microsoft.AspNetCore.SignalR;

namespace OrderingKioskSystem.Application
{
    public class NotificationHub : Hub
    {
        public async Task NotifyStaff(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }
    }
}
