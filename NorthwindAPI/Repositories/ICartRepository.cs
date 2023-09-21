using NorthwindAPI.DTOs;

namespace NorthwindAPI.Repositories
{
    public interface ICartRepository
    {
        void AddItem(CartDTO cartDTO);
        void DeleteItem(int id);
        void UpdateItem(int id, short value);
    }
}
