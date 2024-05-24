using OrderingKioskSystem.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Domain.Entities
{
    [Table("Shipper")]
    public class ShipperEntity : Entity
    {
        public required string Name { get; set; }
        public required string Address { get; set; }
        [MaxLength(11)]
        public required string Phone { get; set; }
        public virtual ICollection<OrderEntity> Orders { get; set; }
        public required string Email { get; set; }
        [ForeignKey(nameof(Email))]
        public virtual UserEntity User { get; set; }
        public bool Status { get; set; }
    }
}
