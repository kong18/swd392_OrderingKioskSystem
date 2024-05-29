using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.User.CreateUser
{
    public class CreateNewUserCommand : IRequest<string>, ICommand
    {
        public CreateNewUserCommand(string email, string password, string role)
        {
            Email = email;
            Password = password;
            Role = role;
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
