using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingKioskSystem.Domain.Entities.Base;

namespace OrderingKioskSystem.Domain.Entities
{
    [Table("Order")]
    public class OrderEntity : Entity
    {
        [Column(TypeName = "decimal(18,4)")]
        public required decimal Total { get; set; }
        public required string Status { get; set; }
        public string Note { get; set; }
        public required string KioskID { get; set; }
        [ForeignKey(nameof(KioskID))]
        public virtual KioskEntity Kiosk { get; set; }
        public string? ShipperID { get; set; }
        [ForeignKey(nameof(ShipperID))]
        public virtual ShipperEntity Shipper { get; set; }
        public virtual ICollection<PaymentEntity> Payments { get; set; }
        public virtual ICollection<OrderDetailEntity> OrderDetails { get; set; }
    }

}
