﻿using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Order.Delete
{
    public class DeleteOrderCommand : IRequest<string>, ICommand
    {
        public DeleteOrderCommand(string id) 
        {
            ID = id;
        }
        public string ID { get; set; }
    }
}
