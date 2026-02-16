using Aplication.Interfaces.IQuery;
using Infraestructure.Perssistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Query
{
    public class OrderItemQuery : IOrderItemQuery
    {
        private readonly AppDbContext _Context;

        public OrderItemQuery(AppDbContext context)
        {
            _Context = context;
        }

        public async Task<bool> OrderItemExists(Guid DishId)
        {
            return await _Context.OrderItems.AnyAsync(oi => oi.Dish == DishId);
        }
    }
}
