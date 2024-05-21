using OrderingKioskSystem.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Domain.Entities
{
    [Table("Category")]
    public class CategoryEntity : BangMaGocEntity
    {
        public required string Url { get; set; }
        public virtual ICollection<ProductEntity> Products { get; set; }
    }
}
