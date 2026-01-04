using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entityes
{
    public class Order
    {
        public long OrderId { get; set; }
        public int DeliveryType { get; set; }
        public string DeliveryTo { get; set; }
        public int OverallStatus { get; set; }
        public string Notes { get; set; }
        public decimal Price { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }


        public DeliveryType TheDeliveryType { get; set; }
        public Status TheStatus { get; set; }
        public List<OrderItem> TheOrderItems { get; set; }
    }
}
