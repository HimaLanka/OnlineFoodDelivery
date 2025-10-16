using OnlineFoodDelivery.Data;
using OnlineFoodDelivery.Model;

namespace OnlineFoodDelivery.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly OnlineFoodDeliveryContext _context;

        public LocationRepository(OnlineFoodDeliveryContext context)
        {
            _context = context;
        }

        public void Add(Location location)
        {
            _context.Locations.Add(location);
            _context.SaveChanges();
        }

        public void Update(Location location)
        {
            _context.Locations.Update(location);
            _context.SaveChanges();
        }

        public void Delete(Location location)
        {
            _context.Locations.Remove(location);
            _context.SaveChanges();
        }
        public Location GetById(int id) => _context.Locations.Find(id);

        public List<Location> GetAll() => _context.Locations.ToList();

        //public bool SaveChanges() => _context.SaveChanges() > 0;
    }

}
