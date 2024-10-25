using Microsoft.EntityFrameworkCore;
using ShopPlase.Data;
using ShopPlase.Interfaces;
using ShopPlase.Models;

namespace ShopPlase.Repository
{
    public class BasketRepository:IBasket
    {
        private ApplicationContext _context;


        public BasketRepository(ApplicationContext context)
        {
            _context = context;
        }
        public void AddProductToBasket(int productId, int quantity)
        {
            Console.WriteLine($"Добавление товара с ID: {productId} и количеством: {quantity}");
            var basketItem = _context.Baskets.FirstOrDefault(b => b.ProductId == productId);
            if (basketItem != null)
            {
                basketItem.Quantity += quantity;
            }
            else
            {
                var newBasketItem = new Basket
                {
                    ProductId = productId,
                    Quantity = quantity
                };
                _context.Baskets.Add(newBasketItem);
            }
            _context.SaveChanges();
        }
        public void RemoveProd(int productId)
        {
            var basketItem = _context.Baskets.FirstOrDefault(b => b.ProductId == productId);
            if (basketItem != null)
            {
                _context.Baskets.Remove(basketItem);
                _context.SaveChanges();
            }
        }
        //public void UpdateBasket(Dictionary<int, int> productQuantities)
        //{
        //    foreach (var item in productQuantities)
        //    {
        //        var basketItem = _context.Baskets.FirstOrDefault(b => b.ProductId == item.Key);
        //        if (basketItem != null)
        //        {
        //            basketItem.Quantity = item.Value;
        //        }
        //    }
        //    _context.SaveChanges();
        //}

        public IEnumerable<Basket> GetBasketItems()
        {
            return _context.Baskets.Include(b => b.Product).ToList();
        }

        public void ClearBasket()
        {
            var basketItems = _context.Baskets.ToList();
            _context.Baskets.RemoveRange(basketItems);
            _context.SaveChanges();
        }

        public decimal GetTotalPrice()
        {
            return _context.Baskets
                .Include(b => b.Product)
                .Sum(b => b.Product.RetailPrice * b.Quantity);
        }
    }
}
