using MediatR;
using Microsoft.AspNetCore.Http;
using OrderingKioskSystem.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Product.Update
{
    public class UpdateProductCommand : IRequest<string>, ICommand
    {
        public UpdateProductCommand() { }
        public UpdateProductCommand(string id, string name, string code, string url, string description, decimal price, bool status, int categoryID,FormFile imageFile)
        {
            ID = id;
            Name = name;
            Code = code;
            Description = description;
            Price = price;
            Status = status;
            CategoryID = categoryID;
            ImageFile = imageFile;
        }

        public string ID { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public bool? Status { get; set; }
        public int? CategoryID { get; set; }
    }
}
