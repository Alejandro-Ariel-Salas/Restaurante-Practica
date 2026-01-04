using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs.Responses
{
    public class OrderDetailsResponse
    {
        public long OrderNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public string? DeliveryTo { get; set; }
        public string? Notes { get; set; }

        public GenericResponse Status { get; set; } = null!;
        public GenericResponse DeliveryType { get; set; } = null!;

        public List<OrderItemResponse> Items { get; set; } = new();

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}
