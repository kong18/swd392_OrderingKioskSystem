using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Business.GetBusinessById
{
    public class GetBusinessByIdQuery : IRequest<BusinessDTO>
    {
        [BindProperty(Name = "id")]
        public string Id { get; set; }  
        public GetBusinessByIdQuery(string id) {
            Id = id;
        }
    }
}
