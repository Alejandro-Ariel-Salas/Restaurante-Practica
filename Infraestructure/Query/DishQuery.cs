using Aplication.Enums;
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
    public class DishQuery : IDishQuery
    {
        private readonly AppDbContext _Context;

        public DishQuery(AppDbContext context)
        {
            _Context = context;
        }

        public async Task<bool> DishExists(string name)
        {
            return await _Context.Dishes.AnyAsync(d => d.Name == name);
        }

        public async Task<bool> DishExists(Guid dishId)
        {
            return await _Context.Dishes.AnyAsync(d => d.DishId == dishId);
        }

        public async Task<Dish?> GetDishById(Guid dishId)
        {
            return await _Context.Dishes.Include(d => d.TheCategory).FirstOrDefaultAsync(d => d.DishId == dishId);
        }

        public async Task<List<Dish>> GetDishesFilter(string? name, int? category, bool isAvailable, SortByPrice sortByPrice)
        {
            var query = _Context.Dishes.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(d => d.Name.Contains(name));
            }

            if (category.HasValue)
            {
                query = query.Where(d => d.Category == category.Value);
            }

            if (isAvailable)
            {
                query = query.Where(d => d.Available == isAvailable);
            }

            if (sortByPrice == SortByPrice.asc)
            {
                query = query.OrderBy(d => d.Price);
            }
            else
            {
                query = query.OrderByDescending(d => d.Price);
            }

            return await query.Include(d => d.TheCategory).ToListAsync();
        }
    }
}
