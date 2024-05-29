using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.User.CreateManager
{
    public class CreateManagerUserCommand : IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string ManagerName { get; set; }
        public string Phone { get; set; }
    }
}
