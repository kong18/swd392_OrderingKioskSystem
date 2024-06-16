using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Category.Create
{
    public class CreateCategoryCommand : IRequest<string>
    {
        public CreateCategoryCommand() { }
        public CreateCategoryCommand(IFormFile imageFile, string name)
        {
            ImageFile = imageFile;
            Name = name;
        }

        public IFormFile ImageFile { get; set; }
        public string Name { get; set; }
    }
}
