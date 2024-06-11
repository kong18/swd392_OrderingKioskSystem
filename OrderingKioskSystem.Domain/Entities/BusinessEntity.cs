using OrderingKioskSystem.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Domain.Entities
{
    [Table("Business")]
    public class BusinessEntity : Entity
    {
        public required string Url { get; set; }
        public required string Name { get; set; }
        public required string BankAccountNumber { get; set; }
        public required string BankAccountName { get; set; }
        public required int BinId {  get; set; }
        public required string BankName { get; set; }
        public virtual ICollection<MenuEntity> Menus { get; set; }
        public virtual ICollection<ProductEntity> Products { get; set; }
        public required string Email { get; set; }
        [ForeignKey(nameof(Email))]
        public virtual UserEntity User { get; set; }
        public bool Status { get; set; }
    }
}
