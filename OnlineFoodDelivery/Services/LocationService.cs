using OnlineFoodDelivery.DTOs;
using OnlineFoodDelivery.Model;
using OnlineFoodDelivery.Repository;

namespace OnlineFoodDelivery.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _repo;

        public LocationService(ILocationRepository repo)
        {
            _repo = repo;
        }

        public void CreateLocation(LocationDto dto)
        {
            var location = new Location
            {
                State = dto.State,
                City = dto.City,
                Area = dto.Area,
                Pincode = dto.Pincode
            };

            _repo.Add(location);
            
        }

        public List<Location> GetAllLocations() => _repo.GetAll();

        public Location GetLocationById(int id) => _repo.GetById(id);

        public bool UpdateLocation(int id, LocationDto dto)
        {
            var location = _repo.GetById(id);
            if (location == null) return false;

            location.State = dto.State;
            location.City = dto.City;
            location.Area = dto.Area;
            location.Pincode = dto.Pincode;

            _repo.Update(location);
            return true;
        }

        public bool DeleteLocation(int id)
        {
            var location = _repo.GetById(id);
            if (location == null) return false;

            _repo.Delete(location);
            return true;
        }
    }

}
