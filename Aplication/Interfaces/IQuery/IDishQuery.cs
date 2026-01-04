using Aplication.Enums;
using Domain.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces.IQuery
{
    public interface IDishQuery
    {
        Task<bool> DishExists(string name);
        Task<bool> DishExists(Guid dishId);
        Task<Dish?> GetDishById(Guid dishId);
        Task<List<Dish>> GetDishesFilter(string? name, int? category, bool isAvailable, SortByPrice sortByPrice);
    }
}
