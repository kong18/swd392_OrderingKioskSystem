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
        public UpdateProductCommand(string Id, string Name, string Code, string Url, string Description, decimal Price, bool Status, int CategoryID,FormFile ImageFile)
        {
            id = Id;
            name = Name;
            code = Code;
            description = Description;
            price = Price;
            status = Status;
            categoryid = CategoryID;
            imagefile = ImageFile;
        }

        public string id { get; set; }
        public string? name { get; set; }
        public string? code { get; set; }
        public IFormFile? imagefile { get; set; }
        public string? description { get; set; }
        public decimal? price { get; set; }
        public bool? status { get; set; }
        public int? categoryid { get; set; }
    }
}
