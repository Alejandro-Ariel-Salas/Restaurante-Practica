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
    public class DishCommand: IDishCommand
    {
        private readonly AppDbContext _Context;

        public DishCommand(AppDbContext context)
        {
            _Context = context;
        }

        public async Task AddDish(Dish dish)
        {             
            _Context.Dishes.Add(dish);
            await _Context.SaveChangesAsync();
        }

        public async Task UpdateDish(Dish dish)
        {
            _Context.Dishes.Update(dish);
            await _Context.SaveChangesAsync();
        }

        public async Task DeleteDish(Dish dish)
        {
            _Context.Dishes.Remove(dish);
            await _Context.SaveChangesAsync();
        }
    }
}
