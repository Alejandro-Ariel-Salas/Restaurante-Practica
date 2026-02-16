using Domain.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces.IQuery
{
    public interface IOrderQuery
    {
        Task<Order> GetOrderById(long orderId);
        Task<List<Order>> GetOrders(DateTime? from, DateTime? to, int? statusId);
    }
}
