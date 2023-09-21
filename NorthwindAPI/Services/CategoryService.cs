using NorthwindAPI.DTOs;
using NorthwindAPI.Models;
using NorthwindAPI.Models.Context;
using NorthwindAPI.Repositories;

namespace NorthwindAPI.Services
{
    public class CategoryService : ICategoryRepository
    {
        private readonly NorthwndContext _context;

        public CategoryService(NorthwndContext context)
        {
            _context = context;
        }

        public List<CategoryDTO> GetAllCategories()
        {
            var categories =from c in _context.Categories
                                 select new CategoryDTO
                                 {
                                     CategoryId = c.CategoryId,
                                     CategoryName = c.CategoryName,
                                     Description = c.Description
        };
            return categories.ToList();
        }
    }
}
