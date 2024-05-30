using MediatR;
using OrderingKioskSystem.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.User.ChangePassword
{
    public class ChangePasswordCommand : IRequest<string>, ICommand
    {
        public ChangePasswordCommand(string oldPassword, string newPassword)
        {
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }
        public ChangePasswordCommand()
        {

        }

        public required string OldPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}
