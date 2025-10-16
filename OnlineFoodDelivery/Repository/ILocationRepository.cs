using OnlineFoodDelivery.Model;

namespace OnlineFoodDelivery.Repository
{
    public interface ILocationRepository
    {
        void Add(Location location);
        Location GetById(int id);
        List<Location> GetAll();
        void Update(Location location);
        void Delete(Location location);
        //bool SaveChanges();
    }

}
