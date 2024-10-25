using Microsoft.AspNetCore.Mvc;
using ShopPlase.Interfaces;
using ShopPlase.Models;
using ShopPlase.Models.Pages;

namespace ShopPlase.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategory _categories;


        public CategoriesController(ICategory categories)
        {
            _categories = categories;
        }
        //public IActionResult Index()
        //{
        //    return View(_categories.GetAllCategories());
        //}
        public IActionResult Index(QueryOptions options)
        {
            return View(_categories.GetCategory(options));
        }
        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            _categories.AddCategory(category);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult EditCategory(long id, QueryOptions options)
        {
            ViewBag.Editid = id;
            return View(nameof(Index), _categories.GetCategory(options));
        }
        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            _categories.UpdateCategory(category);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult UpdateAllCategories(QueryOptions options)
        {
            ViewBag.UpdateAll = true;
            return View(nameof(Index), _categories.GetCategory(options));
        }
        [HttpPost]
        public IActionResult UpdateAllCategories(Category[] categories)
        {
            _categories.UpdateAllCategories(categories);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult DeleteCategory(Category category)
        {
            _categories.DeleteCategory(category);
            return RedirectToAction(nameof(Index));
        }

    }
}
