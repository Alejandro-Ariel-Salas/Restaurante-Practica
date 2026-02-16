using Aplication.DTOs.Request;
using Aplication.DTOs.Responses;
using Aplication.Interfaces.IMappers;
using Domain.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Mappers
{
    public class OrderMapper : IOrderMapper
    {
        public Order GetOrder(OrderRequest orderRequest, decimal price)
        {
            return new Order
            {
                CreateDate = DateTime.UtcNow,
                OverallStatus = 1,
                Price = price,
                DeliveryTo = orderRequest.Delivery.To,
                DeliveryType = orderRequest.Delivery.Id,
                Notes = orderRequest.Notes
            };
        }

        public OrderCreateReponse GetOrderCreateReponse(Order order)
        {
            return new OrderCreateReponse
            {
                OrderNumber = order.OrderId,
                TotalAmount = order.Price,
                CreatedAt = order.CreateDate
            };
        }

        public OrderDetailsResponse GetOrderDetailsResponse(Order order)
        {
            return new OrderDetailsResponse
            {
                OrderNumber = order.OrderId,
                TotalAmount = order.Price,
                DeliveryTo = order.DeliveryTo,
                Notes = order.Notes,
                CreatedAt = order.CreateDate,
                UpdatedAt = order.UpdateDate,
                DeliveryType = new GenericResponse
                {
                    Id = order.TheDeliveryType.Id,
                    Name = order.TheDeliveryType.Name
                },
                Status = new GenericResponse
                {
                    Id = order.TheStatus.Id,
                    Name =  order.TheStatus.Name
                },
                Items = order.TheOrderItems.Select(oi => new OrderItemResponse
                {
                    Id = oi.OrderItemId,
                    Dish = new DishShortResponse {
                        Id = oi.TheDish.DishId,
                        Name = oi.TheDish.Name,
                        Image = oi.TheDish.ImageUrl,
                    },
                    Quantity = oi.Quantity,
                    Notes = oi.Notes,
                    Status = new GenericResponse
                    {
                        Id = oi.TheStatus.Id,
                        Name = oi.TheStatus.Name
                    }
                }).ToList()
            };
        }

        public OrderItem GetOrderItem(Items Item, long orderId)
        {
            return new OrderItem
            {
                Order = orderId,
                Dish = Item.Id,
                Quantity = Item.Quantity,
                Notes = Item.Notes,
                Status = 1,
                CreateDate = DateTime.UtcNow
            };
        }

        public OrderUpdateReponse GetOrderUpdateReponse(Order order)
        {
            return new OrderUpdateReponse
            {
                OrderNumber = order.OrderId,
                TotalAmount = order.Price,
                UpdatedAt = order.UpdateDate
            };
        }
    }
}
