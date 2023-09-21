using NorthwindAPI.DTOs;
using NorthwindAPI.Repositories;
using System.Collections.Generic;

namespace NorthwindAPI.Services
{
    public class CartService:ICartRepository
    {
        public Dictionary<int, CartDTO> MyCart = new Dictionary<int, CartDTO>();
        public void AddItem(CartDTO cartDTO)
        {
            if (MyCart.ContainsKey(cartDTO.Id))
            {
                MyCart[cartDTO.Id].Quantity += 1;
                return;
            }
            MyCart.Add(cartDTO.Id, cartDTO);
        }

        public void DeleteItem(int id)
        {
            if (MyCart.ContainsKey(id))
            {
                MyCart.Remove(id);
            }
        }

        public void UpdateItem(int id, short value)
        {
            if(MyCart.ContainsKey(id))
            {
                MyCart[id].Quantity = value;
            }
        }
    }
}
