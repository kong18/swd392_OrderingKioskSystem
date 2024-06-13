using MediatR;
using Microsoft.AspNetCore.Http;
using OrderingKioskSystem.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Business.Update
{
    public class UpdateBusinessCommand : IRequest<string>, ICommand
    {
        public UpdateBusinessCommand() { }
        public UpdateBusinessCommand(string id, IFormFile? imageFile, string name, string bankAccountNumber, string bankAccountName, string bankName)
        {
            Id = id;
            ImageFile = imageFile;
            Name = name;
            BankAccountNumber = bankAccountNumber;
            BankAccountName = bankAccountName;
            BankName = bankName;
        }

        public string Id { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? Name { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? BankAccountName { get; set; }
        public string? BankName { get; set; }
    }
}
