using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Category.GetAll
{
    public class SearchCategory : IRequest<List<CategoryDTO>>
    {
        public string? Name { get; set; }
        public SearchCategory(string? name = null) {
            Name = name;
        }
    }
}
