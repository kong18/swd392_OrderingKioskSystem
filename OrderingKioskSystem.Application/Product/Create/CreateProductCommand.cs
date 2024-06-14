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
        
        public string name { get; set; }
        public string code { get; set; }
   
        public string description { get; set; }
        public decimal price { get; set; }
        public bool status { get; set; } = true;
        public string categoryname {  get; set; }
        public IFormFile imagefile { get; set; }

    }
}
