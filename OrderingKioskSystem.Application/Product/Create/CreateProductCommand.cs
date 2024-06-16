using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderingKioskSystem.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Product.Create
{
    public class CreateProductCommand : IRequest<string>, ICommand
    {
        public CreateProductCommand() { }
        public CreateProductCommand(string Code,  string Name, string Description, decimal Price, bool Status, string categoryName)
        {
            code = Code;
            name = Name;
            description = Description;
            price = Price;
            status = Status;
            categoryname = categoryName;
        }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; } = true;
        public string CategoryName {  get; set; }
        public IFormFile ImageFile { get; set; }

    }
}
