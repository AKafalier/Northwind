using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindAPI.DTOs;
using NorthwindAPI.Helpers;
using NorthwindAPI.Repositories;
using NorthwindAPI.Services;

namespace NorthwindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public CartController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        //MyCart'ı temsil edern endpoint oluşturun.
        [HttpGet]
        [Route("getitems")]
        public IActionResult GetItems()
        {
            var cartSession = SessionHelper.GetProductFromJson<CartService>(HttpContext.Session, "sepet");
            if (cartSession == null)
            {
                return NotFound("sepeteniz boş!");
            }
            else
            {
                return Ok(cartSession.MyCart.Values.ToList());
            }
        }


        [HttpGet]
        [Route("addtocart/{id}")]
        public IActionResult AddToCart(int id)
        {
            CartService cartService;

            if (SessionHelper.GetProductFromJson<CartService>(HttpContext.Session, "sepet") == null)
            {
                cartService = new CartService();
            }
            else
            {
                cartService = SessionHelper.GetProductFromJson<CartService>(HttpContext.Session, "sepet");
            }

            var product = _productRepository.GetAllProducts().FirstOrDefault(x => x.ProductId == id);

            if (product == null)
            {
                return NotFound("ürün bulunamadı!");
            }
            else
            {

                //araştırma: AutoMapper nedir? ne için kullanılır araştırın.

                //araştırma: AutoFac nedir? ne için kullanılır?

                CartDTO cartDTO = new CartDTO
                {
                    Id = product.ProductId,
                    ProductName = product.ProductName,
                    UnitPrice = product.UnitPrice
                };

                cartService.AddItem(cartDTO);
                SessionHelper.SetJsonProduct(HttpContext.Session, "sepet", cartService);

                return Ok(cartDTO);
            }

        }
        [HttpDelete]
        [Route("deletefromcart/{id}")]
        public IActionResult DeleteItemFromCart(int id)
        {
            var cartService = SessionHelper.GetProductFromJson<CartService>(HttpContext.Session, "sepet");

            if (cartService == null)
            {
                return NotFound("Sepetiniz boş!");
            }

            cartService.DeleteItem(id);
            SessionHelper.SetJsonProduct(HttpContext.Session, "sepet", cartService);

            return Ok($"ID'si {id} olan ürün sepetten silindi.");
        }
        [HttpPut]
        [Route("updatecartitem/{id}")]
        public IActionResult UpdateItemInCart(int id, [FromBody] short value)
        {
            var cartService = SessionHelper.GetProductFromJson<CartService>(HttpContext.Session, "sepet");

            if (cartService == null)
            {
                return NotFound("Sepetiniz boş!");
            }

            cartService.UpdateItem(id, value);
            SessionHelper.SetJsonProduct(HttpContext.Session, "sepet", cartService);

            return Ok($"ID'si {id} olan ürünün adeti {value} olarak güncellendi.");
        }
        [HttpGet]
        [Route("getitemcount")]
        public IActionResult GetItemCount()
        {
            var cartService = SessionHelper.GetProductFromJson<CartService>(HttpContext.Session, "sepet");
            if(cartService != null)
            {
                int cartItemCount = cartService.MyCart.Count;

                return Ok(cartItemCount);
            }
            else
            {
                return Ok("0");
            }
        }

    }
}
