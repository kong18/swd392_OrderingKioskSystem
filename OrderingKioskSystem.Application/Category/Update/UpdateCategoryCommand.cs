using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Category.Update
{
    public class UpdateCategoryCommand : IRequest<string>
    {
        public UpdateCategoryCommand() { }
        public UpdateCategoryCommand(int id, IFormFile imageFile, string name)
        {
            ID = id;
            ImageFile = imageFile;
            Name = name;
        }

        public int ID { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? Name { get; set; }
    }
}
