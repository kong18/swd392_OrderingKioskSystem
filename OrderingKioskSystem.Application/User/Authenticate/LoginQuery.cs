using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.User.Authenticate
{
    public class LoginQuery : IRequest<UserLoginDTO>,IQuery
    {
        public LoginQuery()
        {
        }

        public LoginQuery(string email, string password)
        {
            Email = email;
            Password = password;
        }

        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
