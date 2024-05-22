using AutoMapper;
using OrderingKioskSystem.Application.Business;
using OrderingKioskSystem.Application.Common.Mappings;
using OrderingKioskSystem.Application.ProductMenu;
using OrderingKioskSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Menu
{
    public class MenuDTO : IMapFrom<MenuEntity>
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }
        public string BusinessID { get; set; }
        public BusinessDTO Business { get; set; }
        public List<ProductMenuDTO> ProductMenus { get; set; }

        public void Mapping(Profile profile)
        {
            throw new NotImplementedException();
        }
    }
}
