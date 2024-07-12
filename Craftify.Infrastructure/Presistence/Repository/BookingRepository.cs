using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Domain.Entities;
using Craftify.Infrastructure.Persistence.Repository;

namespace Craftify.Infrastructure.Presistence.Repository
{
    public class BookingRepository(CraftifyDbContext _db) : Repository<Booking>(_db), IBookingRepository
    {
        public void Update(Booking booking)
        {
            _db.Bookings.Update(booking);
        }
    }
}
