using Aplication.Interfaces.IQuery;
using Domain.Entityes;
using Infraestructure.Perssistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Query
{
    public class OrderQuery: IOrderQuery
    {
        private readonly AppDbContext _context;
        public OrderQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderById(long orderId)
        {
            return await _context.Orders.Include(o => o.TheOrderItems).ThenInclude(oi => oi.TheStatus)
                                        .Include(o => o.TheOrderItems).ThenInclude(oi => oi.TheDish)
                                        .Include(o => o.TheStatus)
                                        .Include(o => o.TheDeliveryType)
                                        .Where(o => o.OrderId == orderId).FirstAsync();
        }

        public Task<List<Order>> GetOrders(DateTime? from, DateTime? to, int? statusId)
        {
            var orders = _context.Orders.AsQueryable();

            if (from.HasValue && to.HasValue)
            {
                orders = orders.Where(o => o.CreateDate >= from.Value && o.CreateDate <= to.Value);
            }
            if (statusId.HasValue)
            {
                orders = orders.Where(o => o.OverallStatus == statusId.Value);
            }
            return orders.Include(o => o.TheOrderItems).ThenInclude(oi => oi.TheStatus)
                         .Include(o => o.TheOrderItems).ThenInclude(oi => oi.TheDish)
                         .Include(o => o.TheStatus)
                         .Include(o => o.TheDeliveryType)
                         .ToListAsync();
        }
    }
}
