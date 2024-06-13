using MediatR;
using Microsoft.AspNetCore.Http;
using OrderingKioskSystem.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Business.CreateBusinessCommand
{
    public class CreateBusinessCommand : IRequest<string>, ICommand
    {
        public CreateBusinessCommand() { }
        public CreateBusinessCommand(IFormFile imageFile, string name, int binId, string bankAccountNumber, string bankAccountName, string bankName, string email)
        {
            ImageFile = imageFile;
            Name = name;
            BinId = binId;
            BankAccountNumber = bankAccountNumber;
            BankAccountName = bankAccountName;
            BankName = bankName;
            Email = email;
        }

        public IFormFile ImageFile { get; set; }
        public string Name { get; set; }
        public int BinId { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankAccountName { get; set; }
        public string BankName { get; set; }
        public string Email {  get; set; }
    }
}
