using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs.Responses
{
    public class OrderUpdateReponse
    {
        public long OrderNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
