using Domain.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces.ICommand
{
    public interface IOrderItemCommand
    {
        Task AddOrderItem(OrderItem orderItem);
        Task UpdateOrderItem(OrderItem orderItem);
    }
}
