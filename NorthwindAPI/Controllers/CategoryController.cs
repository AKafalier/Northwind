using Microsoft.AspNetCore.Mvc;
using NorthwindAPI.Repositories;

namespace NorthwindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [HttpGet]
        [Route("listcategories")]
        public IActionResult ListCategories()
        {
            var categories = _categoryRepository.GetAllCategories();
            return Ok(categories);
        }
    }
}
