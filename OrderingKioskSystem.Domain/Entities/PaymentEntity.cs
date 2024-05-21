using OrderingKioskSystem.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Domain.Entities
{
    [Table("Payment")]
    public class PaymentEntity : Entity
    {
        [Column(TypeName = "decimal(18,4)")]
        public required decimal Amount { get; set; }
        public required DateTime PaymentDate { get; set; }
        public required string OrderID { get; set; }
        [ForeignKey(nameof(OrderID))]
        public virtual OrderEntity Order {  get; set; }
        public required int PaymentGatewayID { get; set; }
        [ForeignKey(nameof(PaymentGatewayID))]
        public virtual PaymentGatewayEntity PaymentGateway { get; set; }

    }
}
