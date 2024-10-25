using Microsoft.AspNetCore.Mvc;
using ShopPlase.Interfaces;
using ShopPlase.Models.Pages;
using ShopPlase.Models;

namespace ShopPlase.Controllers
{
    public class UserController : Controller
    {
        private readonly IProduct _products;
        private readonly ICategory _categories;
        public UserController(IProduct products, ICategory categories)
        {
            _products = products;
            _categories = categories;
        }
        public IActionResult Index(QueryOptions options)
        {
            ViewBag.Categories = _categories.GetAllCategories();
            return View(_products.GetProducts(options));
        }


        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            ViewBag.Categories = _categories.GetAllCategories();
            return View(id == 0 ? new Product() : _products.GetProduct(id));
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _products.AddProduct(product);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            if (product.Id == 0)
            {
                _products.AddProduct(product);
            }
            else
            {
                _products.UpdateProduct(product);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult UpdateAll(QueryOptions options)
        {
            ViewBag.Categories = _categories.GetAllCategories().ToList();
            ViewBag.UpdateAll = true;
            return View(nameof(Index), _products.GetProducts(options));

        }
        [HttpPost]
        public IActionResult UpdateAll(Product[] products)
        {
            _products.UpdateAll(products);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult DeleteProduct(Product product)
        {
            _products.DeleteProduct(product);
            return RedirectToAction(nameof(Index));
        }

    }
}
