using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public UpdateBusinessCommand(string id, IFormFile? imageFile, string name, int bankAccountNumber, string bankAccountName, string bankName)
        {
            Id = id;
            ImageFile = imageFile;
            Name = name;
            BankAccountNumber = bankAccountNumber;
            BankAccountName = bankAccountName;
            BankName = bankName;
        }

        [BindProperty(Name = "id")]
        public string Id { get; set; }

        [BindProperty(Name = "image-file")]
        public IFormFile? ImageFile { get; set; }

        [BindProperty(Name = "name")]
        public string? Name { get; set; }

        [BindProperty(Name = "bank-account-number")]
        public int? BankAccountNumber { get; set; }

        [BindProperty(Name = "bank-account-name")]
        public string? BankAccountName { get; set; }

        [BindProperty(Name = "bank-name")]
        public string? BankName { get; set; }
    }
}
