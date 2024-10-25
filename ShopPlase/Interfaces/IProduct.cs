using Microsoft.EntityFrameworkCore;
using ShopPlase.Models;
using ShopPlase.Models.Pages;

namespace ShopPlase.Interfaces
{
    public interface IProduct
    {
        PagedList<Product> GetProducts(QueryOptions options);
        IEnumerable<Product> GetAllProducts();
        void AddProduct(Product product);
        Product GetProduct(int id);
        void UpdateProduct(Product product);
        IEnumerable<Product> GetRandomProducts(int count);

        void UpdateAll(Product[] products);


      void DeleteProduct(Product product);



    }
}
