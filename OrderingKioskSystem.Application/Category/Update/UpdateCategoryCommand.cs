using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Category.Update
{
    public class UpdateCategoryCommand : IRequest<string>
    {
        public UpdateCategoryCommand(int id, string url, string name)
        {
            ID = id;
            Url = url;
            Name = name;
        }

        public int ID { get; set; }
        public string? Url { get; set; }
        public string? Name { get; set; }
    }
}
