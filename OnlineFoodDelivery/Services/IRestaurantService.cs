using OnlineFoodDelivery.DTOs;
using OnlineFoodDelivery.Model;

namespace OnlineFoodDelivery.Services
{
    public interface IRestaurantService
    {
        bool CreateRestaurant(RestaurantDto dto, int ownerId);
        List<Restaurant> GetRestaurantByOwnerId(int ownerId);
        bool UpdateRestaurant(long id, RestaurantDto dto, int ownerId);
        bool DeleteRestaurant(long id, int ownerId);
        List<Restaurant> GetAll();
        List<Restaurant> GetByName(string name);
        List<Restaurant> GetResByLocationId(int locationId);
        List<Restaurant> GetResByState(string locationState);
        List<Restaurant> GetResByCity(string locationCity);
        List<Restaurant> GetResByArea(string locationArea);
        List<Restaurant> GetResByPincode(string locationPincode);
        


    }

}
