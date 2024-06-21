using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderingKioskSystem.Application.Common.Pagination;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Business.GetBusinessByFilter
{
    public class GetBusinessByFilterQuery : IRequest<PagedResult<BusinessDTO>>
    {
        [EmailAddress]
        [BindProperty(Name = "email")]
        public string? Email { get; set; }

        [BindProperty(Name = "name")]
        public string? Name { get; set; }

        [BindProperty(Name = "bank-name")]
        public string? BankName { get; set; }

        [BindProperty(Name = "sort-order")]
        public bool? SortOrder { get; set; }

        [BindProperty(Name = "page-number")]
        public int PageNumber { get; set; } = 1;

        [BindProperty(Name = "page-size")]
        public int PageSize { get; set; } = 10;
    }
}
