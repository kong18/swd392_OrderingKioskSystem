using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.QRCode
{
    public class RegisterWebhookRequest
    {
        public RegisterWebhookRequest(string webhookUrl)
        {
            WebhookUrl = webhookUrl;
        }
        public string WebhookUrl { get; set; }
    }
}
