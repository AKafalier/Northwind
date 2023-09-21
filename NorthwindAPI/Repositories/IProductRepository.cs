using NorthwindAPI.DTOs;

namespace NorthwindAPI.Repositories
{
    public interface IProductRepository
    {
        List<ProductDTO> GetAllProducts();
        ProductDTO GetProductById(int id);
        string DeleteProduct(int id);
        ProductDTO UpdateProduct(int id);
    }
}
