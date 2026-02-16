using Domain.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces.ICommand
{
    public interface IOrderCommand
    {
        Task AddOrder(Order order);
        Task UpdateOrder(Order order);
    }
}
