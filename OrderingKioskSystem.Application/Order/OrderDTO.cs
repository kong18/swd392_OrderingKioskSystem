using OrderingKioskSystem.Application.Kiosk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingKioskSystem.Application.Order
{
    public class OrderDTO
    {
        public string ID { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public string KioskID { get; set; }
        public KioskDTO Kiosk { get; set; }
        public string ShipperID { get; set; }
       
    }
}
