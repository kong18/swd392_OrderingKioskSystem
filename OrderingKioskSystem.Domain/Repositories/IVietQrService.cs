using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Domain.Repositories
{
    public interface IVietQrService
    {
        Task<string> GeneratePaymentQrCode(string accountNo, string accountName, int acqId, int amount, string addInfo, string format = "text", string template = "compact");
    }

}

