using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingKioskSystem.Domain.Entities.Base;

namespace OrderingKioskSystem.Domain.Entities
{
    [Table("Kiosk")]
    public class KioskEntity : Entity
    {
        public required string Location { get; set; }
        public required string Code { get; set; }
        public required int PIN { get; set; }
        public virtual ICollection<OrderEntity> Order { get; set; }
    }
}
