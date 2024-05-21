using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Category.GetAll
{
    public class GetAllCategoryQuery : IRequest<List<CategoryDTO>>
    {
        public GetAllCategoryQuery() { }
    }
}
