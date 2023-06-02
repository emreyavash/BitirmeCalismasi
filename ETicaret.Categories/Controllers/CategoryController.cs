using ETicaret.Categories.Entities;
using ETicaret.Categories.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ETicaret.Categories.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<Category> _logger;

        public CategoryController(ICategoryRepository categoryRepository, ILogger<Category> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        [HttpGet("GetCategories")]
        [ProducesResponseType(typeof(Category), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _categoryRepository.GetCategories();
            return Ok(categories);
        }
        [HttpPost("AddCategory")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddCategory([FromBody]Category category)
        {
            await _categoryRepository.Add(category);
            return Ok("Başarılı bir şekilde eklenmiştir.");
        }
        [HttpDelete("DeleteCategory")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            await _categoryRepository.Delete(id);
            return Ok("Başarılı bir şekilde silindi");
        }
        [HttpPut("UpdateCategory")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateCategory([FromBody] Category category)
        {
            var updatedCategory =await _categoryRepository.Update(category);
            return Ok(updatedCategory);
        }
    }
}
