using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant_API.Models;
using Restaurant_API.Repository;

namespace Restaurant_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IGenericRepository<User> _UserRepo;

        public UserController(IGenericRepository<User> UserRepo)
        {
            _UserRepo = UserRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _UserRepo.GetAll();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _UserRepo.GetById(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            var created = await _UserRepo.Add(user);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User user)
        {
            if (id != user.Id)
                return BadRequest();

            await _UserRepo.Update(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _UserRepo.DeleteById(id);
            return NoContent();
        }
    }
}
