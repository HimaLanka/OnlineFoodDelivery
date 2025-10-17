using OnlineFoodDelivery.Model;

namespace OnlineFoodDelivery.Repository
{
    public interface IMenuCategoryRepository
    {
        public void AddCategory(MenuCategory category);
        public void UpdateCategory(MenuCategory category);
        public void RemoveCategory(MenuCategory category);
        public MenuCategory GetCategoryById(long Cid);
        public List<MenuCategory> AllCategoriesByResId(long Rid);
        //public List<MenuCategory> GetCategoriesWithItemsByResId(long RestaurantId);

    }
}
