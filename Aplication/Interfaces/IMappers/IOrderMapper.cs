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
    public interface IOrderMapper
    {
        OrderCreateReponse GetOrderCreateReponse(Order order);
        OrderDetailsResponse GetOrderDetailsResponse(Order order);
        OrderUpdateReponse GetOrderUpdateReponse(Order order);
        Order GetOrder(OrderRequest orderRequest, decimal price);
        OrderItem GetOrderItem(Items Item, long orderId);
    }
}
