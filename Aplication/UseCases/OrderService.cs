using Aplication.DTOs.Request;
using Aplication.DTOs.Responses;
using Aplication.Exceptions;
using Aplication.Interfaces.ICommand;
using Aplication.Interfaces.IMappers;
using Aplication.Interfaces.IQuery;
using Aplication.Interfaces.IService;
using Domain.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.UseCases
{
    public class OrderService : IOrderService
    {
        private readonly IDishQuery _dishQuery;
        private readonly IOrderQuery _orderQuery;
        private readonly IOrderCommand _orderCommand;
        private readonly IOrderItemCommand _orderItemCommand;
        private readonly IOrderMapper _orderMapper;
        public OrderService(IDishQuery dishQuery, IOrderMapper orderMapper, IOrderCommand orderCommand, IOrderItemCommand orderItemCommand, IOrderQuery orderQuery)
        {
            _dishQuery = dishQuery;
            _orderMapper = orderMapper;
            _orderCommand = orderCommand;
            _orderItemCommand = orderItemCommand;
            _orderQuery = orderQuery;
        }

        public async Task<OrderCreateReponse> CreateOrder(OrderRequest orderRequest)
        {
            decimal totalAmount = 0;

            foreach (var item in orderRequest.Items)
            {
                var dish = await _dishQuery.GetDishById(item.Id);
                if (dish == null && dish.Available)
                {
                    throw new ExceptionBadRequest("El plato especificado no existe o no está disponible");
                }
                if (item.Quantity <= 0)
                {
                    throw new ExceptionBadRequest("La cantidad de un plato debe ser mayor que cero");
                }
                totalAmount += dish.Price * item.Quantity;
            }

            if (orderRequest.Delivery == null)
            {
                throw new ExceptionBadRequest("Se debe especificar el tipo de entrega");
            }

            var order = _orderMapper.GetOrder(orderRequest, totalAmount);
            await _orderCommand.AddOrder(order);

            foreach (var item in orderRequest.Items)
            {
                var orderItem = _orderMapper.GetOrderItem(item, order.OrderId);
                await _orderItemCommand.AddOrderItem(orderItem);
            }
            return _orderMapper.GetOrderCreateReponse(order);
        }

        public async Task<OrderDetailsResponse> GetOrderById(long orderId)
        {
            if (orderId <= 0)
            {
                throw new ExceptionBadRequest("El ID no es valido");
            }
            var order = await _orderQuery.GetOrderById(orderId);

            if (order == null)
            {
                throw new ExceptionNotFound("Orden no encontrada");
            }

            return _orderMapper.GetOrderDetailsResponse(order);
        }

        public async Task<List<OrderDetailsResponse>> GetOrdersByUserId(DateTime? from, DateTime? to, int? statusId)
        {

            if (from.HasValue && to.HasValue && (from.Value > to.Value))
            {
               throw new ExceptionBadRequest("Rango de fechas invalido");
            }

            if (statusId.HasValue && statusId < 1)
            {
                throw new ExceptionBadRequest("El estado no es valido");
            }

            var orders = await _orderQuery.GetOrders(from, to, statusId);

            return orders.Select(o => _orderMapper.GetOrderDetailsResponse(o)).ToList();
        }

        public async Task<OrderUpdateReponse> UpdateOrderItems(OrderUpdateRequest items, long orderId)
        {
            var order = await _orderQuery.GetOrderById(orderId);
            decimal totalAmount = 0;
            List<Items> validItems = new List<Items>();

            if (order == null)
            {
                throw new ExceptionNotFound("Orden no encontrada");
            }

            foreach (var item in items.Items)
            {
                foreach (var orderItem in order.TheOrderItems)
                {
                    if (orderItem.TheDish.DishId == item.Id)
                    {
                        orderItem.Quantity = item.Quantity;
                        orderItem.Notes = item.Notes;
                        await _orderItemCommand.UpdateOrderItem(orderItem);
                        totalAmount += orderItem.TheDish.Price * item.Quantity;
                        validItems.Add(item);
                    }
                }
            }
            foreach (var item in items.Items)
            {
                if(!validItems.Any(vi => vi.Id == item.Id))
                {
                    var dish = await _dishQuery.GetDishById(item.Id);
                    if (dish == null && dish.Available)
                    {
                        throw new ExceptionBadRequest("El plato especificado no existe o no está disponible");
                    }
                    if (item.Quantity <= 0)
                    {
                        throw new ExceptionBadRequest("La cantidad de un plato debe ser mayor que cero");
                    }
                    totalAmount += dish.Price * item.Quantity;
                    var orderItem = _orderMapper.GetOrderItem(item, order.OrderId);
                    await _orderItemCommand.AddOrderItem(orderItem);
                }
            }

            order.Price = totalAmount;
            await _orderCommand.UpdateOrder(order);
            return _orderMapper.GetOrderUpdateReponse(order);
        }

        public async Task<OrderUpdateReponse> UpdateOrderStatus(long orderId, long orderItemId, OrderItemUpdateRequest status)
        {
            var order = await _orderQuery.GetOrderById(orderId);
            int newStatus = 5;

            if (order == null)
            {
                throw new ExceptionNotFound("Orden no encontrada");
            }

            foreach (var item in order.TheOrderItems)
            {
                if (item.OrderItemId == orderItemId)
                {
                    item.Status = status.Status;
                    await _orderItemCommand.UpdateOrderItem(item);
                }
            }
            foreach (var item in order.TheOrderItems)
            {
                if (item.Status < newStatus)
                {
                    newStatus = item.Status;
                }
            }
            order.OverallStatus = newStatus;
            order.UpdateDate = DateTime.Now;
            await _orderCommand.UpdateOrder(order);
            return _orderMapper.GetOrderUpdateReponse(order);
        }
    }
}
