using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant_API.DTOS.ProductsDTO;
using Restaurant_API.Models;
using Restaurant_API.Repository;

namespace Restaurant_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepo;
        IMapper mapper;
        public ProductsController(IProductRepository productRepo ,IMapper _mapper)
        {
            _productRepo = productRepo;
            this.mapper = _mapper;
        
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var  products = await _productRepo.GetAllWithCategory();    

            var productsDTO = mapper.Map<List<ReadProduct>>(products);
            return Ok(productsDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productRepo.GetByIdWithCategory(id);
            var productDTO = mapper.Map<ReadProduct>(product);
            if (productDTO == null) return NotFound();
            return Ok(productDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var created = await _productRepo.Add(product);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (id != product.Id) return BadRequest();
            await _productRepo.Update(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public  async Task<IActionResult> Delete(int id)
        {
            await _productRepo.DeleteById(id);
            return NoContent();
        }

    }
}
