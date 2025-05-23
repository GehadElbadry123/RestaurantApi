using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant_API.DTOS.CategoryDTO;
using Restaurant_API.DTOS.ProductsDTO;
using Restaurant_API.Models;
using Restaurant_API.Repository;

namespace Restaurant_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;
        IMapper mapper;
        public CategoryController(ICategoryRepository categoryRepo, IMapper _mapper)
        {
            _categoryRepo = categoryRepo;
            this.mapper = _mapper;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryRepo.GetAllWithProductCount();

            var categoriesDTO = mapper.Map<List<ReadCategory>>(categories);
            return Ok(categoriesDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryRepo.GetByIdWithProductCount(id); 

            var categoryDTO = mapper.Map<ReadCategory>(category);
            if (categoryDTO == null) return NotFound();
            return Ok(categoryDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            var created = await _categoryRepo.Add(category);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Category category)
        {
            if (id != category.Id) return BadRequest();
            await _categoryRepo.Update(category);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryRepo.DeleteById(id);
            return NoContent();
        }
    }
}
