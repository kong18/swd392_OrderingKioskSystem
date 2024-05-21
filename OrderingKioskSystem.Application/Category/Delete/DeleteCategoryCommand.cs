using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Category.Delete
{
    public class DeleteCategoryCommand : IRequest<string>
    {
        public DeleteCategoryCommand(int id) 
        {
            ID = id;
        }
        public int ID { get; set; }
    }
}
