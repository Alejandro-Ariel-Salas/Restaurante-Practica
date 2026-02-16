using Aplication.DTOs.Request;
using Aplication.DTOs.Responses;
using Aplication.Enums;
using Aplication.Exceptions;
using Aplication.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PracticaRestaurante.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }

        [HttpPost("Dish")]
        [ProducesResponseType(typeof(DishResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateDish([FromBody] DishRequest dishRequest)
        {
            try
            {
                var dish = await _dishService.CreateDish(dishRequest);
                return Ok(dish);
            }
            catch (ExceptionBadRequest ex)
            {
                return BadRequest(new ApiError { Message = ex.Message });
            }
            catch (ExceptionNotFound ex)
            {
                return NotFound(new ApiError { Message = ex.Message });
            }
            catch (ExceptionConflict ex)
            {
                return Conflict(new ApiError { Message = ex.Message });
            }
        }

        [HttpPut("Dish/{id}")]
        [ProducesResponseType(typeof(DishResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateDish(Guid id, [FromBody] DishUpdateRequest dishUpdateRequest)
        {
            try
            {
                var dish = await _dishService.UpdateDish(id, dishUpdateRequest);
                return Ok(dish);
            }
            catch (ExceptionBadRequest ex)
            {
                return BadRequest(new ApiError { Message = ex.Message });
            }
            catch (ExceptionNotFound ex)
            {
                return NotFound(new ApiError { Message = ex.Message });
            }
            catch (ExceptionConflict ex)
            {
                return Conflict(new ApiError { Message = ex.Message });
            }
        }

        [HttpGet("Dish")]
        [ProducesResponseType(typeof(List<DishResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetDishesFilther([FromQuery] string? name, [FromQuery] int? category, [FromQuery] bool onlyActive = true, [FromQuery] SortByPrice sortByPrice = SortByPrice.asc)
        {
            try
            {
                var dishes = await _dishService.GetDishesFilther(name, category, onlyActive, sortByPrice);
                return Ok(dishes);
            }
            catch (ExceptionBadRequest ex)
            {
                return BadRequest(new ApiError { Message = ex.Message });
            }
        }

        [HttpGet("Dish/{id}")]
        [ProducesResponseType(typeof(DishResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetDishById(Guid id)
        {
            try
            {
                var dish = await _dishService.GetDishById(id);
                return Ok(dish);
            }
            catch (ExceptionNotFound ex)
            {
                return NotFound(new ApiError { Message = ex.Message });
            }
            catch (ExceptionBadRequest ex)
            {
                return BadRequest(new ApiError { Message = ex.Message });
            }
        }

        [HttpDelete("Dish/{id}")]
        [ProducesResponseType(typeof(DishResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteDish(Guid id)
        {
            try
            {
                var dish = await _dishService.DeleteDish(id);
                return Ok(dish);
            }
            catch (ExceptionNotFound ex)
            {
                return NotFound(new ApiError { Message = ex.Message });
            }
            catch (ExceptionConflict ex)
            {
                return Conflict(new ApiError { Message = ex.Message });
            }
        }

        [HttpGet("Category")]
        [ProducesResponseType(typeof(List<CategoryResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _dishService.GetCategories();
            return Ok(categories);
        }

        [HttpGet("Status")]
        [ProducesResponseType(typeof(List<GenericResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllStatus()
        {
            var statuses = await _dishService.GetAllStatus();
            return Ok(statuses);
        }

        [HttpGet("DeliveryType")]
        [ProducesResponseType(typeof(List<GenericResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllDeliveryTypes()
        {
            var deliveryTypes = await _dishService.GetAllDeliveryTypes();
            return Ok(deliveryTypes);
        }
    }
}
