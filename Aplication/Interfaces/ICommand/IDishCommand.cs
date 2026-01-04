using Domain.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces.ICommand
{
    public interface IDishCommand
    {
        Task AddDish(Dish dish);
        Task UpdateDish(Dish dish);
        Task DeleteDish(Dish dish);
    }
}
