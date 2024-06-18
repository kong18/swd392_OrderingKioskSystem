﻿using SWD.OrderingKioskSystem.Application.VNPay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.Payment
{
    public interface IPaymentService
    {
        Task SavePaymentAsync(TransactionResponsePaymentDTO response);
    }
}