using AutoMapper;
using MediatR;
using OrderingKioskSystem.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.Dashboard
{
    public class GetSalesDataQuery : IRequest<SalesDataDTO> { }



}
