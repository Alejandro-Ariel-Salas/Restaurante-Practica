using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs.Responses
{
    public class OrderCreateReponse
    {
        public long OrderNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public GenericResponse Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }

}
