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
        public CreateCategoryCommand(string Url, string Name)
        {
            url = Url;
            name = Name;
        }

        public string url { get; set; }
        public string name { get; set; }
    }
}
