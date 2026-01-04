using Aplication.DTOs.Request;
using Aplication.DTOs.Responses;
using Aplication.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces.IService
{
    public interface IDishService
    {
        Task<DishResponse> CreateDish(DishRequest dishRequest);
        Task<List<DishResponse>> GetDishesFilther(string? name, int? category, bool onlyActive, SortByPrice sortByPrice);
        Task<DishResponse> UpdateDish(Guid id, DishUpdateRequest dishUpdateRequest);
    }
}
