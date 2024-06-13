using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.User.Authenticate
{
    public class LoginGoogleCheckAuthenQuery : IRequest<bool>
    {
        public LoginGoogleCheckAuthenQuery() { }
        public LoginGoogleCheckAuthenQuery(string email)
        {
            Email = email;
        }
        public string Email { get; set; }
    }
}
