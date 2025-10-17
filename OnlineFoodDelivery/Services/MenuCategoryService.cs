using OnlineFoodDelivery.Data;
using OnlineFoodDelivery.DTOs;
//using OnlineFoodDelivery.Migrations;
using OnlineFoodDelivery.Model;
using OnlineFoodDelivery.Repository;
using OnlineFoodDelivery.Exceptions;

namespace OnlineFoodDelivery.Services
{
    public class MenuCategoryService:IMenuCategoryService
    {
        private readonly IMenuCategoryRepository _repo;
        

        public MenuCategoryService(IMenuCategoryRepository repo)
        {
            _repo = repo;
           
        }
        public bool AddCategory1(MenuCategoryDto category)
        {
            var categ = new MenuCategory
            {
                CategoryName = category.CategoryName,
                RestaurantId = category.RestaurantId
            };

            _repo.AddCategory(categ);
            return true;
        }
        public bool UpdateCategory1(long Cid, MenuCategoryDto category)
        {
            var categ=_repo.GetCategoryById(Cid);
            if (categ == null) 
            {
                throw new CategoryNotFoundException($"Category with category id {Cid} not found.");
            }

            categ.RestaurantId=category.RestaurantId;
            categ.CategoryName=category.CategoryName;   

            _repo.UpdateCategory(categ);
            return true;
        }
        public bool RemoveCategory1(long Cid)
        {
            var categ = _repo.GetCategoryById(Cid);
            if (categ == null)
            {
                throw new CategoryNotFoundException($"Category with category id {Cid} not found.");
            }
            _repo.RemoveCategory(categ);
            return true;
        }
        public MenuCategory GetCategoryById1(long Cid)
        {
            return _repo.GetCategoryById(Cid);
        }
        public List<MenuCategory> AllCategoriesByResId1(long Rid)
        {
            return _repo.AllCategoriesByResId(Rid);
        }
    }
}
