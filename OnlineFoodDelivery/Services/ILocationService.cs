using OnlineFoodDelivery.DTOs;
using OnlineFoodDelivery.Model;

namespace OnlineFoodDelivery.Services
{
    public interface ILocationService
    {
        void CreateLocation(LocationDto dto);
        List<Location> GetAllLocations();
        Location GetLocationById(int id);
        bool UpdateLocation(int id, LocationDto dto);
        bool DeleteLocation(int id);
    }

}
