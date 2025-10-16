using OnlineFoodDelivery.Model;

namespace OnlineFoodDelivery.Repository
{
    public interface IMenuItemRepository
    {
        public void AddItem(MenuItem item);
        public void UpdateItem(MenuItem item);
        public void RemoveItem(MenuItem item);
        public MenuItem GetItemById(long Itemid);
        public MenuItem GetItemByName(string Name);
        public List<MenuItem> GetAllItems();
        public List<MenuItem> GetItemsByCategoryId(long Cid);
        //public List<MenuItem> GetItemsByRestaurantId(long Rid);


    }
}
