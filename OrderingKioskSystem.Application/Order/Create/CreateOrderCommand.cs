﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Order.Create
{
    public class CreateOrderCommand : IRequest<CreateOrderResponse>
    {
        public CreateOrderCommand(string kioskID, List<RequestItem> items)
        {
            KioskID = kioskID;
            Items = items;
        }
        public string KioskID { get; set; }
        public string? Note {  get; set; }
        public List<RequestItem> Items { get; set; }
    }

    public class RequestItem
    {
        public string ProductID { get; set; }
        public int Quantity { get; set; }
        public string? Size { get; set; }
    }

    public class CreateOrderResponse
    {
        public string KioskID { get; set; }
        public string OrderId { get; set; }
        public decimal Total { get; set; }
        public List<ResponseItem>? Items { get; set; }
    }

    public class ResponseItem
    {
        public string ProductID { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string? Size { get; set; }
    }


}
