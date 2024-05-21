using OrderingKioskSystem.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Domain.Entities
{
    [Table("Menu")]
    public class MenuEntity : Entity
    {
        public required string Name { get; set; }
        public required MenuType Type { get; set; }
        public required bool Status { get; set; }
        public virtual ICollection<ProductMenuEntity> ProductMenus { get; set; }
    }

    public enum MenuType
    {
        Morning,
        Afternoon,
        Evening
    }
}
