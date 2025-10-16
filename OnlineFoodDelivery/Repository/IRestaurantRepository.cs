using OnlineFoodDelivery.Model;

namespace OnlineFoodDelivery.Repository
{
    public interface IRestaurantRepository
    {
        public void Add(Restaurant restaurant);
        public void Update(Restaurant restaurant);
        public void Delete(Restaurant restaurant);
        public List<Restaurant> GetAllByOwnerId(int ownerId);
        public Restaurant GetByRestaurantId(long id);
        public List<Restaurant> GetAll();
        public List<Restaurant> GetByName(string name);
        public List<Restaurant> GetResByLocationId(int locationId);
        public List<Restaurant> GetResByState(string locationState);
        public List<Restaurant> GetResByCity(string locationCity);
        public List<Restaurant> GetResByArea(string locationArea);
        public List<Restaurant> GetResByPincode(string locationPincode);
        
        
    }

}
