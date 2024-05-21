using OrderingKioskSystem.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Domain.Entities
{
    [Table("PaymentGateway")]
    public class PaymentGatewayEntity : BangMaGocEntity
    {
        public virtual ICollection<PaymentEntity> Payments { get; set; }
    }
}
