using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Category.Create
{
    public class CreateCategoryCommand : IRequest<string>
    {
        public CreateCategoryCommand(string url, string name)
        {
            Url = url;
            Name = name;
        }

        public string Url { get; set; }
        public string Name { get; set; }
    }
}
