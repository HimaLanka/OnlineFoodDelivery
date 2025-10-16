using OnlineFoodDelivery.Model;
using OnlineFoodDelivery.DTOs;

namespace OnlineFoodDelivery.Services
{
    public interface IMenuItemService
    {
        public bool AddItem1(MenuItemDto item);
        public bool UpdateItem1(long Iid, MenuItemDto item);
        public bool RemoveItem1(long Iid);
        public MenuItem GetItemById1(long Itemid);
        public MenuItem GetItemByName1(string Name);
        public List<MenuItem> GetAllItems1();
        public List<MenuItem> GetItemsByCategoryId1(long Cid);
    }
}
