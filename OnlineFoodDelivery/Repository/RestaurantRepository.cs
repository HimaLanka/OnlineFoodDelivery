using OnlineFoodDelivery.Data;
using OnlineFoodDelivery.Model;
using Microsoft.EntityFrameworkCore;
using OnlineFoodDelivery.Repository;


namespace OnlineFoodDelivery.Repository
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly OnlineFoodDeliveryContext _context;

        public RestaurantRepository(OnlineFoodDeliveryContext context)
        {
            _context = context;
        }

        public void Add(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();
        }

        public void Update(Restaurant restaurant)
        {
            _context.Restaurants.Update(restaurant);
            _context.SaveChanges();
        }

        public void Delete(Restaurant restaurant)
        {
            _context.Restaurants.Remove(restaurant);
            _context.SaveChanges();
        }

        //public Restaurant GetByOwnerId(int ownerId) =>
        //_context.Restaurants.Include(r => r.Location).FirstOrDefault(r => r.UserId == ownerId);
        public List<Restaurant> GetAllByOwnerId(int ownerId)
        {
            return _context.Restaurants
                .Where(r => r.Id == ownerId)
                .ToList();
        }

        public Restaurant GetByRestaurantId(long id) =>
            _context.Restaurants.Include(r => r.Location).FirstOrDefault(r => r.RestaurantId == id);
        /*.Include(r => r.Location): Tells Entity Framework to eagerly load the related Location
         * entity for the restaurant. Without this, Location would be null unless lazy loading is enabled.
         * .FirstOrDefault(r => r.UserId == ownerId): Returns the first restaurant found where
         * UserId matches the given ownerId. If none is found, it returns null.*/

        public List<Restaurant> GetAll() =>
            _context.Restaurants.Include(r => r.Location).ToList();

        public List<Restaurant> GetByName(string name) =>
            _context.Restaurants.Include(r => r.Location).Where(r => r.ResName.Contains(name)).ToList();

        public List<Restaurant> GetResByLocationId(int locationId) =>
            _context.Restaurants.Include(r => r.Location).Where(r => r.LocationId == locationId).ToList();
        
        public List<Restaurant> GetResByState(string state)
        {
            return _context.Restaurants.Include(r => r.Location).Where(r => r.Location.State.ToLower().Contains(state.Trim().ToLower())).ToList();
            // here r means it takes as restaurant, we can't use r.state, we have to use r.Location.State
            //Because State is not a property of Restaurant — it belongs to the related Location entity.
            //So you must go through r.Location.State.
        }

        public List<Restaurant> GetResByCity(string city)
        {
            return _context.Restaurants.Include(r => r.Location).Where(r => r.Location.City.ToLower().Contains(city.Trim().ToLower())).ToList();
        }

        public List<Restaurant> GetResByArea(string area)
        {
            return _context.Restaurants.Include(r => r.Location).Where(r => r.Location.Area.ToLower().Contains(area.Trim().ToLower())).ToList();
        }

        public List<Restaurant> GetResByPincode(string pincode)
        {
            return _context.Restaurants.Include(r => r.Location).Where(r => r.Location.Pincode.ToLower().Contains(pincode.Trim().ToLower())).ToList();
        }

        

        
    }

}
