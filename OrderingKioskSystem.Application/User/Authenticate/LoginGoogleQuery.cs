﻿using MediatR;
using OrderingKioskSystem.Application.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.OrderingKioskSystem.Application.User.Authenticate
{
    public class LoginGoogleQuery : IRequest<UserLoginDTO>
    {
        public LoginGoogleQuery() { }
        public LoginGoogleQuery(string email)
        {
            Email = email;
        }

        public string Email { get; set; }
    }
}