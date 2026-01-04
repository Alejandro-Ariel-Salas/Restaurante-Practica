using Aplication.DTOs.Request;
using Aplication.DTOs.Responses;
using Domain.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces.IMappers
{
    public interface IDishMapper
    {
        Dish createDish(DishRequest dish);
        DishResponse createDishResponse(Dish dish);
    }
}
