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
    public class OrderCommand : IOrderCommand
    {
        private readonly AppDbContext _context;

        public OrderCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}
