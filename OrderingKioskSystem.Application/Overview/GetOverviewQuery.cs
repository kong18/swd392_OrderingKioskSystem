﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Overview
{
    public class GetOverviewQuery : IRequest<List<OverviewDTO>>
    {
    }
}