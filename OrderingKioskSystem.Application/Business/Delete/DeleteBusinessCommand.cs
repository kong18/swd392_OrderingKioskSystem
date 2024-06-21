using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Business.Delete
{
    public class DeleteBusinessCommand : IRequest<string>
    {
        [BindProperty(Name = "id")]
        public string Id {  get; set; }
        public DeleteBusinessCommand(string id ) {
            Id = id;
        }
    }
}
