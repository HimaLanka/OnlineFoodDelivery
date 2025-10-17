using OnlineFoodDelivery.DTOs;
using OnlineFoodDelivery.Model;
using OnlineFoodDelivery.Repository;
using OnlineFoodDelivery.Data;

namespace OnlineFoodDelivery.Services
{
    public class RestaurantService : IRestaurantService

    {

        private readonly IRestaurantRepository _repo;

        private readonly OnlineFoodDeliveryContext _context;

        public RestaurantService(IRestaurantRepository repo, OnlineFoodDeliveryContext context)

        {

            _repo = repo;

            _context = context;

        }

        public bool CreateRestaurant(RestaurantDto dto)

        {

            // Check if restaurant already exists for this owner

            //if (_repo.GetByOwnerId(ownerId) != null) return false;

            // Validate location exists

            var location = _context.Locations.Find(dto.LocationId);

            if (location == null) return false;

            var restaurant = new Restaurant

            {

                ResName = dto.ResName,

                Image = dto.Image,

                LocationId = dto.LocationId,

                Id = dto.Id,

                DeliveryCharges = dto.DeliveryCharges

            };

            _repo.Add(restaurant);

            return true;

            //return _repo.SaveChanges();

        }

        public List<Restaurant> GetRestaurantByOwnerId(int ownerId) => _repo.GetAllByOwnerId(ownerId);

        public bool UpdateRestaurant(long id, RestaurantDto dto, int ownerId)

        {

            var restaurant = _repo.GetByRestaurantId(id);

            if (restaurant == null || restaurant.Id != ownerId) return false;

            // Optional: Validate new location exists

            var location = _context.Locations.Find(dto.LocationId);

            if (location == null) return false;

            restaurant.ResName = dto.ResName;

            restaurant.Image = dto.Image;

            restaurant.LocationId = dto.LocationId;

            restaurant.DeliveryCharges = dto.DeliveryCharges;

            _repo.Update(restaurant);

            return true;

        }

        public bool DeleteRestaurant(long id, int ownerId)

        {

            var restaurant = _repo.GetByRestaurantId(id);

            if (restaurant == null || restaurant.Id != ownerId) return false;

            _repo.Delete(restaurant);

            return true;

        }

        public List<Restaurant> GetAll() => _repo.GetAll();

        public List<Restaurant> GetByName(string name) => _repo.GetByName(name);

        public List<Restaurant> GetResByLocationId(int locationId) => _repo.GetResByLocationId(locationId);

        public List<Restaurant> GetResByState(string locationState) => _repo.GetResByState(locationState);

        public List<Restaurant> GetResByCity(string locationCity) => _repo.GetResByCity(locationCity);

        public List<Restaurant> GetResByArea(string locationArea) => _repo.GetResByArea(locationArea);

        public List<Restaurant> GetResByPincode(string locationPincode) => _repo.GetResByPincode(locationPincode);



    }

}
