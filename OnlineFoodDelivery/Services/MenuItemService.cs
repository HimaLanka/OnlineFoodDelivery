using OnlineFoodDelivery.DTOs;
using OnlineFoodDelivery.Model;
using OnlineFoodDelivery.Repository;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OnlineFoodDelivery.Services
{
    public class MenuItemService:IMenuItemService
    {
        private readonly IMenuItemRepository _repo;


        public MenuItemService(IMenuItemRepository repo)
        {
            _repo = repo;

        }
        public bool AddItem1(MenuItemDto item)
        {
            var item1 = new MenuItem
            {
                ItemName = item.ItemName,
                ItemPrice = item.ItemPrice,
                ItemDescription = item.ItemDescription,
                ItemImg = item.ItemImg,
                IsAvailable = item.IsAvailable,
                IsVeg=item.IsVeg,
                CategoryId = item.CategoryId
            };

            _repo.AddItem(item1);
            return true;
        }
        public bool UpdateItem1(long Iid, MenuItemDto item)
        {
            var item2 = _repo.GetItemById(Iid);
            if (item2 == null) { return false; }
            item2.ItemName= item.ItemName;
            item2.ItemPrice= item.ItemPrice;
            item2.ItemDescription= item.ItemDescription;
            item2.ItemImg = item.ItemImg;
            item2.IsAvailable = item.IsAvailable;
            item2.IsVeg= item.IsVeg;
            item2.CategoryId = item.CategoryId;

            _repo.UpdateItem(item2);
            return true;
        }
        public bool RemoveItem1(long Iid)
        {
            var item = _repo.GetItemById(Iid);
            if (item == null) return false;
            _repo.RemoveItem(item);
            return true;
        }
        public MenuItem GetItemById1(long Itemid)
        {
            return _repo.GetItemById(Itemid);
        }
        public MenuItem GetItemByName1(string Name)
        {
            return _repo.GetItemByName(Name);
        }
        public List<MenuItem> GetAllItems1()
        {
            return _repo.GetAllItems();
        }
        public List<MenuItem> GetItemsByCategoryId1(long Cid)
        {
            return _repo.GetItemsByCategoryId(Cid);
        }
    }
}
