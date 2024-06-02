using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingKioskSystem.Domain.Entities.Base;

namespace OrderingKioskSystem.Domain.Entities
{
    [Table("ProductMenu")]
    public class ProductMenuEntity 
    {
        public ProductMenuEntity()
        {
            ID = Guid.NewGuid().ToString("N");
        }

        [Key]
        public string ID { get; set; }
        public required string MenuID { get; set; }
        public required string ProductID { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public required decimal Price { get; set; }
        public required DateTime CreatedDate { get; set; }

        [ForeignKey("MenuID")]
        public virtual MenuEntity Menu { get; set; }

        [ForeignKey("ProductID")]
        public virtual ProductEntity Product { get; set; }
    }
}
