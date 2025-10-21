using OnlineFoodDelivery.Data;
using OnlineFoodDelivery.Model;
using Microsoft.EntityFrameworkCore;

namespace OnlineFoodDelivery.Repository
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly OnlineFoodDeliveryContext _context;

        public MenuItemRepository(OnlineFoodDeliveryContext context)
        {
            _context = context;
        }
        public void AddItem(MenuItem item)
        {
            _context.MenuItem.Add(item);
            _context.SaveChanges();
        }
        public void UpdateItem(MenuItem item)
        {
            _context.MenuItem.Update(item);
            _context.SaveChanges();
        }
        public void RemoveItem(MenuItem item)
        {
            _context.MenuItem.Remove(item);
            _context.SaveChanges();
        }
        public MenuItem GetItemById(long Itemid)
        {
            return _context.MenuItem.FirstOrDefault(r => r.MenuItemId == Itemid);
        }
        public MenuItem GetItemByName(string Name)
        {
            return _context.MenuItem.FirstOrDefault(r => r.ItemName == Name);

        }
        public List<MenuItem> GetAllItems()
        {
            return _context.MenuItem.ToList();
        }
        public List<MenuItem> GetItemsByCategoryId(long Cid)
        {
            return _context.MenuItem.Where(r => r.CategoryId == Cid).ToList();
        }

        public List<MenuItem> SearchItemsWithRestaurant(string itemName)
        {
            return _context.MenuItem
                .Include(i => i.Category)
                    .ThenInclude(c => c.Restaurant)
                        .ThenInclude(r => r.Location)
                .Where(i => i.ItemName.ToLower().Contains(itemName.ToLower()))
                .ToList();
        }

        public bool CategoryExists(long categoryId)
        {
            return _context.MenuCategories.Any(c => c.CategoryId == categoryId);
        }

    }
}
