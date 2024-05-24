using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Domain.Entities
{
    [Table("User")]
    public class UserEntity
    {
        [Key]
        public required string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public virtual BusinessEntity Business { get; set; }
        public virtual ShipperEntity Shipper { get; set; }
        public virtual ManagerEntity Manager { get; set;}
    }
}
