using ShopPlase.Models;
using ShopPlase.Models.Pages;

namespace ShopPlase.Interfaces
{
    public interface ICategory
    {
        PagedList<Category> GetCategory(QueryOptions options);
        IEnumerable<Category> GetAllCategories();
        void AddCategory(Category category);
        Category GetCategory(int id);
        void UpdateCategory(Category category);
        void UpdateAllCategories(Category[] categories);
        void DeleteCategory(Category category);

    }
}
