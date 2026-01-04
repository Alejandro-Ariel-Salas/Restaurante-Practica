using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs.Request
{
    public class OrderRequest
    {
        public List<Items> Items { get; set; } = new();
        public Delivery Delivery { get; set; } = null!;
        public string? Notes { get; set; }
    }
}
