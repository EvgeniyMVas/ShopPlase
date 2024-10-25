namespace ShopPlase.Models
{
    public class Product
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        //public string Category { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal RetailPrice { get; set; }
        public int CategoryId {  get; set; }
        public Category Category { get; set; }
        public string ImageUrl { get; set; } = "~/imgs/default.jpg";
    }
}
