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
        public CreateProductCommand(string code,  string name, string description, decimal price, bool status, string categoryName, IFormFile imageFile)
        {
            Code = code;
            Name = name;
            Description = description;
            Price = price;
            Status = status;
            CategoryName = categoryName;
            ImageFile = imageFile;
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
