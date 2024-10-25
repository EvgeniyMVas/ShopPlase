using Microsoft.AspNetCore.Mvc;
using ShopPlase.Interfaces;
using ShopPlase.Models;
using ShopPlase.Repository;

namespace ShopPlase.Controllers
{
    public class BasketController : Controller
    {
        private readonly IProduct _products;
        private readonly IBasket _basket;


        public BasketController(IProduct products, IBasket baskets)
        {
            _products = products;
            _basket = baskets;
        }
        public IActionResult Index()
        {
            var basketItems = _basket.GetBasketItems();
            return View(basketItems);
        }
        
        [HttpGet]
        public IActionResult AddToBasket(int id, int quantity = 1)
        {
            var product = _products.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            _basket.AddProductToBasket(id, quantity);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult RemoveFromBasket(int productId)
        {
            _basket.RemoveProd(productId);
            return RedirectToAction(nameof(Index));
        }
        //[HttpPost]
        //public IActionResult UpdateBasket(Dictionary<int, Basket> basketItems)
        //{
        //    var quantities = basketItems.ToDictionary(b => b.Key, b => b.Value.Quantity);
        //    _basket.UpdateBasket(quantities);
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
