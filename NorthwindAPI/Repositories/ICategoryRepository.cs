using NorthwindAPI.DTOs;

namespace NorthwindAPI.Repositories
{
    public interface ICategoryRepository
    {
        List<CategoryDTO> GetAllCategories();
    }
}
