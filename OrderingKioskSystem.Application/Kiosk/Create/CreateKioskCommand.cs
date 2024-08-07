﻿using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Kiosk.Create
{
    public class CreateKioskCommand : IRequest<string>, ICommand
    {
        public CreateKioskCommand(string location)
        {
            Location = location;
        }
        public string Location { get; set; }


    }
}
