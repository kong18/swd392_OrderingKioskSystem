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
    [Table("OrderDetail")]
    public class OrderDetailEntity 
    {
        public OrderDetailEntity()
        {
            ID = Guid.NewGuid().ToString("N");
        }

        [Key]
        public string ID { get; set; }
        public required string OrderID { get; set; }
        public required string ProductID { get; set; }
        public required int Quantity { get; set; }
        public required decimal UnitPrice { get; set;}
        public required decimal Price { get; set;}

        [ForeignKey("OrderID")]
        public virtual OrderEntity Order { get; set; }

        [ForeignKey("ProductID")]
        public virtual ProductEntity Product { get; set; }
        public string? Size { get; set; }
        public required DateTime OrderDate { get; set; }
        public required bool Status { get; set; }
    }

}
