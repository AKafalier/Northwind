using Microsoft.AspNetCore.Mvc;

namespace Northwind.Layout.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
