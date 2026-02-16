using Domain.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces.IQuery
{
    public interface IDeliveryTypeQuery
    {
        Task<List<DeliveryType>> GetDeliveryTypes();
    }
}
