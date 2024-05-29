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
    [Table("Manager/Staff")]
    public class ManagerEntity : Entity
    {
       
        public string Name { get; set; }
        public string Phone { get; set; }
        public required string Email { get; set; }
        [ForeignKey(nameof(Email))]
        public virtual UserEntity User { get; set; }
    }
}
