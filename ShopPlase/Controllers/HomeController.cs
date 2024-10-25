using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopPlase.Interfaces;
using ShopPlase.Models;
using ShopPlase.Models.Pages;
using System.Diagnostics;

namespace ShopPlase.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProduct _products;
        private readonly ICategory _categories;
        public HomeController(IProduct products, ICategory categories)
        {
            _products = products;
            _categories = categories;
        }


        //    [HttpGet]
        //    public IActionResult Index()
        //    {
        //        //Console.Clear();
        //        return View(_products.GetAllProducts());
        //    }


        //    [HttpPost]
        //    public IActionResult AddProduct(Product product)
        //    {
        //        _products.AddProduct(product);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    [HttpGet]
        //    public IActionResult UpdateProduct(int id)
        //    {
        //        return View(_products.GetProduct(id));
        //    }
        //    [HttpPost]
        //    public IActionResult UpdateProduct(Product product)
        //    {
        //        _products.UpdateProduct(product);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    [HttpGet]
        //    public IActionResult UpdateAll()
        //    {
        //        ViewBag.UpdateAll = true;
        //        return View(nameof(Index), _products.GetAllProducts());
        //    }
        //    [HttpPost]
        //    public IActionResult UpdateAll(Product[] products)
        //    {
        //        _products.UpdateAll(products);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    [HttpPost]
        //    public IActionResult DeleteProduct(Product product)
        //    {
        //        _products.DeleteProduct(product);
        //        return RedirectToAction(nameof(Index));
        //    }
        //[HttpGet]
        //public IActionResult Index()
        //{
        //    ViewBag.Categories = _categories.GetAllCategories();
        //    return View(_products.GetAllProducts());
        //}
        public IActionResult Index()
        {
            var randomProducts = _products.GetRandomProducts(6);

            var viewModel = new HomeViewModel
            {
                LatestProducts = randomProducts.ToList(),
            };

            return View(viewModel);
        }

        public IActionResult Details(int id)
        {
            var product= _products.GetProduct(id);
            return View(product);
        }
       


    }

}
