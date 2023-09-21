using Microsoft.AspNetCore.Mvc;

namespace Northwind.Layout.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}