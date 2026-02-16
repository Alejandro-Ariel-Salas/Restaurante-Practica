using Aplication.DTOs.Request;
using Aplication.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces.IService
{
    public interface IOrderService
    {
        Task<OrderCreateReponse> CreateOrder(OrderRequest orderRequest);
        Task<List<OrderDetailsResponse>> GetOrdersByUserId(DateTime? from, DateTime? to, int? statusId);
        Task<OrderUpdateReponse> UpdateOrderItems (OrderUpdateRequest items, long orderId);
        Task<OrderDetailsResponse> GetOrderById(long orderId);
        Task<OrderUpdateReponse> UpdateOrderStatus(long orderId, long orderItemId, OrderItemUpdateRequest status);
    }
}
