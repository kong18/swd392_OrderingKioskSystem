using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Menu.Update
{
    public class UpdateMenuCommand : IRequest<string>
    {
        public UpdateMenuCommand(string iD, string title, string name, bool status, string type, string businessID)
        {
            ID = iD;
            Title = title;
            Name = name;
            Status = status;
            Type = type;
            BusinessID = businessID;
        }

        public string ID { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Type { get; set; }
        public string? BusinessID { get; set; }
    }
}
