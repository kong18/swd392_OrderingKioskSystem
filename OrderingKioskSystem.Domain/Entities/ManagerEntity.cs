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
    public class ManagerEntity
    {
        protected ManagerEntity()
        {
            ID = Guid.NewGuid().ToString("N");
        }
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public required string Email { get; set; }
        [ForeignKey(nameof(Email))]
        public virtual UserEntity User { get; set; }
    }
}
