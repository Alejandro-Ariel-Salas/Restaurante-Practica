using Aplication.DTOs.Request;
using Aplication.DTOs.Responses;
using Aplication.Enums;
using Aplication.Exceptions;
using Aplication.Interfaces.ICommand;
using Aplication.Interfaces.IMappers;
using Aplication.Interfaces.IQuery;
using Aplication.Interfaces.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.UseCases
{
    public class DishService : IDishService
    {
        private readonly IDishMapper _dishMapper;
        private readonly IDishQuery _dishQuery;
        private readonly IDishCommand _dishCommand;
        private readonly ICategoryQuery _categoryQuery;
        private readonly IOrderItemQuery _orderItemQuery;
        private readonly IStatusQuery _statusQuery;
        private readonly IDeliveryTypeQuery _deliveryTypeQuery;

        public DishService(IDishMapper dishMapper, IDishQuery dishQuery, IDishCommand dishCommand, ICategoryQuery categoryQuery, IOrderItemQuery orderItemQuery, IStatusQuery statusQuery, IDeliveryTypeQuery deliveryTypeQuery)
        {
            _dishMapper = dishMapper;
            _dishQuery = dishQuery;
            _dishCommand = dishCommand;
            _categoryQuery = categoryQuery;
            _orderItemQuery = orderItemQuery;
            _statusQuery = statusQuery;
            _deliveryTypeQuery = deliveryTypeQuery;
        }

        public async Task<DishResponse> CreateDish(DishRequest dishRequest)
        {
            await ValidateDish(dishRequest);

            var dish = _dishMapper.createDish(dishRequest);
            await  _dishCommand.AddDish(dish);

            dish = await _dishQuery.GetDishById(dish.DishId);
            return _dishMapper.createDishResponse(dish);
        }

        public async Task<List<DishResponse>> GetDishesFilther(string? name, int? category, bool onlyActive, SortByPrice sortByPrice)
        {
            if(!string.IsNullOrEmpty(name) && category.HasValue)
            {
                throw new ExceptionBadRequest("Parámetros de ordenamiento inválidos");
            }
            var dishes = await _dishQuery.GetDishesFilter(name, category, onlyActive, sortByPrice);
            var dishResponses = dishes.Select(d => _dishMapper.createDishResponse(d)).ToList();

            return dishResponses;
        }

        public async Task<DishResponse> UpdateDish(Guid id, DishUpdateRequest dishUpdateRequest)
        {
            await  ValidateDish(id, dishUpdateRequest);

            var dish = await _dishQuery.GetDishById(id);

            dish.Name = dishUpdateRequest.Name;
            dish.Description = dishUpdateRequest.Description;
            dish.Price = dishUpdateRequest.Price;
            dish.Category = dishUpdateRequest.Category;
            dish.ImageUrl = dishUpdateRequest.Image;
            dish.Available = dishUpdateRequest.IsActive;
            dish.UpdateDate = DateTime.UtcNow;

            await _dishCommand.UpdateDish(dish);

            dish = await _dishQuery.GetDishById(dish.DishId);
            return _dishMapper.createDishResponse(dish);
        }

        public async Task ValidateDish(DishRequest dish)
        {
            bool dishExist = await _dishQuery.DishExists(dish.Name);
            bool categoryExist = await _categoryQuery.CategoryExists(dish.Category);

            if (dishExist)
            {
                throw new ExceptionConflict("Ya existe un plato con ese nombre");
            }
            if (!categoryExist)
            {
                throw new ExceptionBadRequest("La categoria especificada no existe.");
            }
            if (dish.Price < 0)
            {
                throw new ExceptionBadRequest("El precio debe ser mayor a cero");
            }
        }

        public async Task ValidateDish(Guid id, DishUpdateRequest dishUpdate)
        {
            var dish = await _dishQuery.GetDishById(id);
            bool nameExist = await _dishQuery.DishExists(dishUpdate.Name);
            bool categoryExist = await _categoryQuery.CategoryExists(dishUpdate.Category);

            if (dish == null) {
                throw new ExceptionNotFound("El plato que intenta modificar no existe");
            }
            if (nameExist && dishUpdate.Name != dish.Name)
            {
                throw new ExceptionConflict("Ya existe un plato con ese nombre");
            }
            if (!categoryExist)
            {
                throw new ExceptionBadRequest("La categoria especificada no existe.");
            }
            if (dish.Price < 0)
            {
                throw new ExceptionBadRequest("El precio debe ser mayor a cero");
            }
        }

        // 2do TP

        public async Task<DishResponse> GetDishById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ExceptionBadRequest("Formato de ID inválido");
            }

            var dish =  await _dishQuery.GetDishById(id);
            if (dish == null)
            {
                throw new ExceptionNotFound("Plato no encontrado");
            }

            return _dishMapper.createDishResponse(dish);
        }

        public async Task<DishResponse> DeleteDish(Guid id)
        {
            var dish = await _dishQuery.GetDishById(id);
            if (dish == null)
            {
                throw new ExceptionNotFound("Plato no encontrado");
            }

            var orderItemsWithDish = await _orderItemQuery.OrderItemExists(id);
            if (orderItemsWithDish)
            {
                dish.Available = false;
                await _dishCommand.UpdateDish(dish);
                throw new ExceptionConflict("No se puede eliminar el plato porque está asociado a uno o más items de pedido.");
            }

            await _dishCommand.DeleteDish(dish);
            return _dishMapper.createDishResponse(dish);
        }

        public async Task<List<CategoryResponse>> GetCategories()
        {
            var categories = await _categoryQuery.GetAllCategorys();
            return categories.Select(c => _dishMapper.createCategoryResponse(c)).ToList();
        }

        public Task<List<GenericResponse>> GetAllStatus()
        {
            var statuses =  _statusQuery.GetAllStatuses();
            return statuses.ContinueWith(t => t.Result.Select(s => _dishMapper.createGenericResponse(s)).ToList());
        }
        public Task<List<GenericResponse>> GetAllDeliveryTypes()
        {
            var deliveryTypes =  _deliveryTypeQuery.GetDeliveryTypes();
            return deliveryTypes.ContinueWith(t => t.Result.Select(d => _dishMapper.createGenericResponse(d)).ToList());
        }
    }
}
