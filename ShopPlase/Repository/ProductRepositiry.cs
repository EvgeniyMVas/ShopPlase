using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using ShopPlase.Data;
using ShopPlase.Interfaces;
using ShopPlase.Models;
using ShopPlase.Models.Pages;

namespace ShopPlase.Repository
{
    public class ProductRepository : IProduct
    {
        private readonly ApplicationContext _context;

        public ProductRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void AddProduct(Product product)
        {
            if (string.IsNullOrEmpty(product.ImageUrl))
            {
                product.ImageUrl = "~/imgs/default.jpg";
            }
            _context.Add(product);
            _context.SaveChanges(); 
        }
        public PagedList<Product> GetProducts(QueryOptions options)
        {
            return new PagedList<Product>(_context.Products.Include(e => e.Category), options);
        }


        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.Include(e=>e.Category); 
        }
        public Product GetProduct(int id)
        {
            //return _context.Products.Find(id);
            return _context.Products
        .Include(p => p.Category)
        .FirstOrDefault(p => p.Id == id);
        }
      


        public void UpdateProduct(Product product)
        {
            //_context.Products.Update(product);
            Product product2 = GetProduct(product.Id);
            product2.Name = product.Name;
            //product2.Category = product.Category;
            product2.CategoryId= product.CategoryId;
            product2.RetailPrice = product.RetailPrice;
            product2.PurchasePrice = product.PurchasePrice;
            if (string.IsNullOrEmpty(product2.ImageUrl))
            {
                product2.ImageUrl = "~/imgs/default.jpg";
            }
            _context.SaveChanges();
        }

        public void UpdateAll(Product[] products)
        {
          
            Dictionary<int, Product> data = products.ToDictionary(e => e.Id);
            IEnumerable<Product> baseline = _context.Products.Where(e => data.Keys.Contains(e.Id));
            foreach (Product product in baseline)
            {
                Product requestProduct = data[product.Id];
                product.Name = requestProduct.Name;
                product.CategoryId = requestProduct.CategoryId;
                product.RetailPrice = requestProduct.RetailPrice;
                product.PurchasePrice = requestProduct.PurchasePrice;
                if (string.IsNullOrEmpty(product.ImageUrl))
                {
                    product.ImageUrl = "~/imgs/default.jpg";
                }
            }
            _context.SaveChanges();

        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
        public IEnumerable<Product> GetRandomProducts(int count)
        {
            return _context.Products
                .Include(p => p.Category)
                .OrderBy(p => Guid.NewGuid()) 
                .Take(count)
                .ToList();
        }

    }

}
