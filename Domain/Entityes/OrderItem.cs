using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entityes
{
    public class OrderItem
    {
        public long OrderItemId { get; set; }
        public long Order { get; set; }
        public Guid Dish { get; set; }
        public int Quantity { get; set; }
        public string Notes { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }


        public Dish TheDish { get; set; }
        public Status TheStatus { get; set; }
        public Order TheOrder { get; set; }
    }
}
