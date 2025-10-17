using OnlineFoodDelivery.DTOs;
using OnlineFoodDelivery.Model;

namespace OnlineFoodDelivery.Services
{
    public interface IMenuCategoryService
    {
        public bool AddCategory1(MenuCategoryDto category);
        public bool UpdateCategory1(long Cid, MenuCategoryDto category);
        public bool RemoveCategory1(long Cid);
        public MenuCategory GetCategoryById1(long Cid);
        public List<MenuCategory> AllCategoriesByResId1(long Rid);
        
    }
}
