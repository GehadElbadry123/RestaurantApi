using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant_API.DTOS.CategoryDTO;
using Restaurant_API.DTOS.OrderDTO;
using Restaurant_API.Models;
using Restaurant_API.Repository;

namespace Restaurant_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderRepository _orderRepo;
        IMapper mapper;

        public OrderController(IOrderRepository orderRepo , IMapper mapper)
        {
            _orderRepo = orderRepo;
            this.mapper = mapper;   
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderRepo.GetAllWithDetails();  

            var ordersDTO = mapper.Map<List<ReadOrder>>(orders);
            return Ok(ordersDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderRepo.GetByIdWithDetails(id);
            var orderDTO = mapper.Map<ReadOrder>(order);
            if (orderDTO == null)
                return NotFound();
            return Ok(orderDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            var created = await _orderRepo.Add(order);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Order order)
        {
            if (id != order.Id)
                return BadRequest();

            await _orderRepo.Update(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderRepo.DeleteById(id);
            return NoContent();
        }
    }
}
