using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs.Request
{
    public class OrderUpdateRequest
    {
        public List<Items> Items { get; set; } = new();
    }

    public class Items
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public string? Notes { get; set; }
    }
}
