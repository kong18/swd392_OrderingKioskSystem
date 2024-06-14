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
    [Table("Product")]
    public class ProductEntity : Entity
    {
        
        public required string Code { get; set; }
        public required string Url {  get; set; }
        [MaxLength(100)]
        public required string Name { get; set; }
        [MaxLength(150)]
        public required string Description { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public required decimal Price { get; set; }
        public required bool Status { get; set; }
        public required int CategoryID { get; set; }
        [ForeignKey(nameof(CategoryID))]
        public virtual CategoryEntity Category { get; set; }
        public required string BusinessID { get; set; }
        [ForeignKey(nameof(BusinessID))]
        public virtual BusinessEntity Business { get; set; }
        public virtual ICollection<OrderDetailEntity> OrderDetails { get; set; }
        public virtual ICollection<ProductMenuEntity> ProductMenus { get; set; }
    }
}
