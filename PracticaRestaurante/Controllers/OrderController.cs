using Aplication.DTOs.Request;
using Aplication.DTOs.Responses;
using Aplication.Exceptions;
using Aplication.Interfaces.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PracticaRestaurante.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderCreateReponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest orderRequest)
        {
            try
            {
                var orderResponse = await _orderService.CreateOrder(orderRequest);
                return Ok(orderResponse);
            }
            catch (ExceptionBadRequest ex)
            {
                return BadRequest(new ApiError { Message = ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<OrderDetailsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOrdersByUserId([FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] int? status)
            {
            try
            {
                var orders = await _orderService.GetOrdersByUserId(from, to, status);
                return Ok(orders);
            }
            catch (ExceptionBadRequest ex)
            {
                return BadRequest(new ApiError { Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderDetailsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrderById(long id)
        {
            try
            {
                var order = await _orderService.GetOrderById(id);
                return Ok(order);
            }
            catch (ExceptionNotFound ex)
            {
                return NotFound(new ApiError { Message = ex.Message });
            }
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(OrderUpdateReponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOrderItems(long id, [FromBody] OrderUpdateRequest items)
        {
            try
            {
                var updatedOrder = await _orderService.UpdateOrderItems(items, id);
                return Ok(updatedOrder);
            }
            catch (ExceptionBadRequest ex)
            {
                return BadRequest(new ApiError { Message = ex.Message });
            }
        }

        [HttpPatch("{id}/item/{itemId}")]
        [ProducesResponseType(typeof(OrderUpdateReponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOrderStatus(long id, long itemId, [FromBody] OrderItemUpdateRequest status)
        {
            try
            {
                var updatedOrder = await _orderService.UpdateOrderStatus(id, itemId, status);
                return Ok(updatedOrder);
            }
            catch (ExceptionBadRequest ex)
            {
                return BadRequest(new ApiError { Message = ex.Message });
            }
            catch (ExceptionNotFound ex)
            {
                return NotFound(new ApiError { Message = ex.Message });
            }
        }
    }
}
