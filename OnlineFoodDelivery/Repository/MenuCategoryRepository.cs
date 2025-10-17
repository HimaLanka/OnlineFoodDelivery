using OnlineFoodDelivery.Data;
using OnlineFoodDelivery.Model;
using System.Linq;

namespace OnlineFoodDelivery.Repository
{
    public class MenuCategoryRepository:IMenuCategoryRepository
    {
        private readonly OnlineFoodDeliveryContext _context;

        public MenuCategoryRepository(OnlineFoodDeliveryContext context)
        {
            _context = context;
        }
        public void AddCategory(MenuCategory category)
        {
            _context.MenuCategories.Add(category);
            _context.SaveChanges();
        }
        public void UpdateCategory(MenuCategory category)
        {
            _context.MenuCategories.Update(category);
            _context.SaveChanges();
        }
        public void RemoveCategory(MenuCategory category)
        {
            _context.MenuCategories.Remove(category);
            _context.SaveChanges();
        }
        public MenuCategory GetCategoryById(long Cid)
        {
            return _context.MenuCategories.FirstOrDefault(r=>r.CategoryId==Cid);
        }
        public List<MenuCategory> AllCategoriesByResId(long Rid)
        {
            return _context.MenuCategories.Where(r => r.RestaurantId == Rid).ToList();
        }




    }
}
