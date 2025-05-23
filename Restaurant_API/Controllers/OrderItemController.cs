using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant_API.DTOS.OrderDTO;
using Restaurant_API.DTOS.OrderItemDTO;
using Restaurant_API.Models;
using Restaurant_API.Repository;

namespace Restaurant_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemRepository _orderItemRepo;
        IMapper mapper;
        public OrderItemController(IOrderItemRepository orderItemRepo , IMapper mapper)
        {
            _orderItemRepo = orderItemRepo;
            this.mapper = mapper;   
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _orderItemRepo.GetAllWithProduct();
            var ordersDTO = mapper.Map<List<ReadOrderItem>>(items);
            return Ok(ordersDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _orderItemRepo.GetByIdWithProduct(id);
            var orderDTO = mapper.Map<ReadOrderItem>(item);
            if (orderDTO == null)
                return NotFound();
            return Ok(orderDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderItem item)
        {
            var created = await _orderItemRepo.Add(item);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OrderItem item)
        {
            if (id != item.Id)
                return BadRequest();

            await _orderItemRepo.Update(item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderItemRepo.DeleteById(id);
            return NoContent();
        }
    }
}
