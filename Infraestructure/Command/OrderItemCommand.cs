using Aplication.Interfaces.ICommand;
using Domain.Entityes;
using Infraestructure.Perssistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Command
{
    public class OrderItemCommand: IOrderItemCommand
    {
        private readonly AppDbContext _context;

        public OrderItemCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddOrderItem(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderItem(OrderItem orderItem)
        {
            _context.OrderItems.Update(orderItem);
            await _context.SaveChangesAsync();
        }
    }
}
