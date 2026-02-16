using Aplication.DTOs.Request;
using Aplication.DTOs.Responses;
using Aplication.Interfaces.ICommand;
using Aplication.Interfaces.IMappers;
using Aplication.Interfaces.IQuery;
using Domain.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Mappers
{
    public class DishMapper : IDishMapper
    {
        public CategoryResponse createCategoryResponse(Category category)
        {
            return new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Order = category.Order,
            };
        }

        public Dish createDish(DishRequest dish)
        {
            return new Dish
            {
                DishId = Guid.NewGuid(),
                Name = dish.Name,
                Description = dish.Description,
                Price = dish.Price,
                Category = dish.Category,
                Available = true,
                ImageUrl = dish.Image,
                CreateDate = DateTime.UtcNow
            };
        }

        public DishResponse createDishResponse(Dish dish)
        {
            return new DishResponse
            {
                Id = dish.DishId,
                Name = dish.Name,
                Description = dish.Description,
                Price = dish.Price,
                Image = dish.ImageUrl,
                Category = new GenericResponse
                {
                    Id = dish.TheCategory.Id,
                    Name = dish.TheCategory.Name
                },
                IsActive = dish.Available,
                CreatedAt = dish.CreateDate,
                UpdatedAt = dish.UpdateDate
            };
        }

        public GenericResponse createGenericResponse(Status status)
        {
            return new GenericResponse
            {
                Id = status.Id,
                Name = status.Name
            };
        }

        public GenericResponse createGenericResponse(DeliveryType deliveryType)
        {
            return new GenericResponse
            {
                Id = deliveryType.Id,
                Name = deliveryType.Name
            };
        }
    }
}
