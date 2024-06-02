using AutoMapper;
using OrderingKioskSystem.Application.Order;
using OrderingKioskSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Menu
{
    public static class MenuDTOMappingExtension
    {
        public static MenuDTO MapToMenuDTO(this MenuEntity menuEntity, IMapper mapper)
        {
            return mapper.Map<MenuDTO>(menuEntity);
        }

        public static List<MenuDTO> MapToMenuDTOList(this IEnumerable<MenuEntity> menus, IMapper mapper)
        {
            return menus.Select(menu => menu.MapToMenuDTO(mapper)).ToList();
        }
    }
}
