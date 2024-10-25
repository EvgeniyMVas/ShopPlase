using ShopPlase.Models;

namespace ShopPlase.Interfaces
{
    public interface IBasket
    {
        void AddProductToBasket(int productId, int quantity);
        void RemoveProd(int productId);
        IEnumerable<Basket> GetBasketItems();
        void ClearBasket();
        decimal GetTotalPrice();
        //void UpdateBasket(Dictionary<int, int> productQuantities);
    }
}
